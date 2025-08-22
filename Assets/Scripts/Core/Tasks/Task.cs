using System.Linq;
using UnityEngine;
using DIALOGUE;
using System.Collections;

[System.Serializable]
public class Task
{
    [SerializeField] public string _id;
    public string ID => _id;
    public string DisplayName => _id;

    [SerializeField] public TaskCondition[] _conditions;
    [SerializeField] public TaskDependency[] _dependencies;
    [SerializeField] public string _onCompleteCommandNames;

    private DL_COMMAND_DATA _commandData;
    private bool _isActive;
    private CommandManager _commandManager => CommandManager.instance;

    public bool IsActive => _isActive;
    public bool IsCompleted => _isActive && _conditions.All(c => c.IsMet());

    public void Initialize()
    {
        if (!AreDependenciesMet()) return;

        _commandData = new DL_COMMAND_DATA(_onCompleteCommandNames);

        foreach (var condition in _conditions)
        {
            condition.RegisterListeners();
        }
        _isActive = true;
    }

    public void Cleanup()
    {
        foreach (var condition in _conditions)
        {
            condition.UnregisterListeners();
        }
        _isActive = false;
    }

    public bool AreDependenciesMet()
    {
        return _dependencies.All(d =>
            TaskManager.Instance.allTasks.TryGetValue(d.requiredTaskID, out Task task) &&
            task.IsCompleted
        );
    }

    public bool CheckCompletion()
    {
        return IsCompleted;
    }

    public void ExecuteRewards()
    {
        TaskManager.Instance.StartCoroutine(ExecutingRewards());
    }

    private IEnumerator ExecutingRewards()
    {
        _isActive = false;

        foreach (var condition in _conditions)
        {
            condition.UnregisterListeners();
        }

        if (_commandData?.commands == null) yield break;

        foreach (var command in _commandData.commands)
        {
            var cw = _commandManager.Execute(command.name, command.arguments);

            if (command.waitForCompletion || command.name == "wait")
            {
                while (!cw.IsDone)
                {
                    yield return null;
                }
            }
        }
    }

    private IEnumerator CompleteAsync()
    {
        yield return TaskManager.Instance.StartCoroutine(ExecutingRewards());
        Cleanup();
    }

    public void Complete()
    {
        if (!_isActive) return;
        TaskManager.Instance.StartCoroutine(CompleteAsync());
    }
}