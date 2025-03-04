using UnityEngine;

[System.Serializable]
public class TaskDependency
{
    public string requiredTaskID;
    public bool mustCompleteBefore;

    public bool IsDependencyMet()
    {
        if (!TaskManager.Instance.allTasks.ContainsKey(requiredTaskID))
            return false;

        var task = TaskManager.Instance.allTasks[requiredTaskID];
        return mustCompleteBefore ? task.isActive : !task.isActive;
    }
}