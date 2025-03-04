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
            DontDestroyOnLoad(gameObject);
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

    public void AddTaskByTask(Task task)
    {
        if (!allTasks.ContainsKey(task.ID))
        {
            activeTasks.Add(task);
            allTasks.Add(task.ID, task);
            task.Initialize();
        }
    }

    public void CheckTaskProgress()
    {
        foreach (var task in activeTasks.ToArray())
        {
            if (!task.AreDependenciesMet())
            {
                Debug.Log(task.taskName + "not complete");

                continue;
            }

            if (task.CheckCompletion())
            {
                task.ExecuteRewards();
                activeTasks.Remove(task);
                allTasks.Remove(task.ID);
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