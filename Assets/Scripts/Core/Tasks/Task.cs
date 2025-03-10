using System.Linq;
using UnityEngine.Events;
using UnityEngine;
using Unity.VisualScripting;
using DIALOGUE;
using System;
using UnityEditor.U2D.Aseprite;
using System.Collections.Generic;
using static DL_COMMAND_DATA;
using System.Collections;

[System.Serializable]
public class Task
{
    public string ID;
    public string taskName;

    public TaskCondition[] conditions;
    //public UnityEvent onComplete;
    //private UnityAction _onCompleteAction;
    public TaskDependency[] dependencies;
    private CommandManager commandManager => CommandManager.instance;

    public string onCompleteCommandNames = null;
    private DL_COMMAND_DATA commandData = null;
    public bool isActive;

    public bool isCompleted => isActive && conditions.All(c => c.IsMet());

    public void Initialize()
    {
        if (!AreDependenciesMet()) return;

        commandData = new DL_COMMAND_DATA(onCompleteCommandNames);

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
        TaskManager.Instance.StartCoroutine(ExecutingRewards());
    }

    private IEnumerator ExecutingRewards()
    {
        //EventCenter.Instance.taskEvents[ID]?.Invoke();
        isActive = false;
        foreach (var condition in conditions)
        {
            condition.UnregisterListeners();
        }

        List<DL_COMMAND_DATA.Command> commands = commandData.commands;
        foreach (DL_COMMAND_DATA.Command command in commands)
        {
            if (command.waitForCompletion || command.name == "wait")
            {
                CoroutineWrapper cw = CommandManager.instance.Execute(command.name, command.arguments);
                while (!cw.IsDone)
                {
                    yield return null;
                }
            }
            else
                CommandManager.instance.Execute(command.name, command.arguments);
        }
    }

    public IEnumerator CompleteAsync()
    {
        if (!isActive) yield break;

        yield return TaskManager.Instance.StartCoroutine(ExecutingRewards());

        isActive = false;
    }

    public void Complete()
    {
        if (!isActive) return;

        TaskManager.Instance.StartCoroutine(CompleteAsync());
    }
}