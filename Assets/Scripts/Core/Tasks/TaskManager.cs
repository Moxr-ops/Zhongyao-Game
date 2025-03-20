using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    [SerializeField] List<Task> activeTasks = new List<Task>();
    public Dictionary<string, Task> allTasks => new Dictionary<string, Task>();
    List<ScriptableObject> allObjects => new List<ScriptableObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddTaskByName(string taskName)
    {
        TaskData taskDataAsset = Resources.Load<TaskData>(FilePaths.GetPathToResource(FilePaths.resources_task, taskName));

        if (taskDataAsset != null)
        {
            Task task = taskDataAsset.GetTask();
            AddTaskByTask(task);
        }
        else
        {
            Debug.LogWarning($"Task with name {taskName} not found in resources.");
        }
    }

    public void RemoveTaskByName(string taskName)
    {
        if (allTasks.TryGetValue(taskName, out Task task))
        {
            activeTasks.Remove(task);
            allTasks.Remove(taskName);

            task.Cleanup();
            Debug.Log($"Task {taskName} removed successfully.");
        }
        else
        {
            Debug.LogWarning($"Task with name {taskName} not found.");
        }
    }

    private void AddTaskByTask(Task task)
    {
        if (!allTasks.ContainsKey(task.ID))
        {
            activeTasks.Add(task);
            allTasks.Add(task.ID, task);
            task.Initialize();
        }
    }

    private void RemoveTaskByTask(Task task)
    {
        if (allTasks.ContainsKey(task.ID))
        {
            activeTasks.Remove(task);
            allTasks.Remove(task.ID);
        }
    }

    public void CheckTaskProgress()
    {
        foreach (var task in activeTasks.ToArray())
        {
            if (!task.AreDependenciesMet())
            {
                Debug.Log(task.ID + " not met");

                continue;
            }

            if (task.CheckCompletion())
            {
                Debug.Log(task.ID + " complete");
                task.Complete();

            }
            else
            {
                Debug.Log(task.ID + " not complete");
            }
        }
    }

    public void SaveTasks()
    {
        // ´æµµ
    }

    public void LoadTasks()
    {
        // ¶Áµµ
    }
}