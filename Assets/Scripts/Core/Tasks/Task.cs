using System.Linq;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class Task
{
    public string ID;
    public string taskName;
    public TaskCondition[] conditions;
    public UnityEvent onComplete;
    public TaskDependency[] dependencies;
    public bool isActive;

    public bool isCompleted => isActive && conditions.All(c => c.IsMet());

    public void Initialize()
    {
        if (!AreDependenciesMet()) return;

        EventCenter.Subscribe(ID, );

        foreach (var condition in conditions)
        {
            condition.RegisterListeners();
        }
        isActive = true;
    }

    public bool AreDependenciesMet()
    {
        return dependencies.All(d =>
            TaskManager.Instance.allTasks.ContainsKey(d.requiredTaskID) &&
            !TaskManager.Instance.allTasks[d.requiredTaskID].isActive
        );
    }

    public bool CheckCompletion()
    {
        return isActive && conditions.All(c => c.IsMet());
    }

    public void ExecuteRewards()
    {
        onComplete?.Invoke();
        EventCenter.Instance.taskEvents[ID]?.Invoke();
        isActive = false;
        foreach (var condition in conditions)
        {
            condition.UnregisterListeners();
        }
    }

    [ContextMenu("Test Complete")]
    private void OnComplete()
    {
        UnityEngine.Debug.Log($"Task {taskName} completed!");
    }
}