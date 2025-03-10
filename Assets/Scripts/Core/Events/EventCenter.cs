using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : MonoBehaviour
{
    public static EventCenter Instance;

    public Dictionary<string, UnityEvent> taskEvents = new Dictionary<string, UnityEvent>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public static void Subscribe(string taskID, UnityAction action)
    {
        if (Instance == null)
        {
            Debug.LogError("EventCenter instance is not initialized.");
            return;
        }

        if (string.IsNullOrEmpty(taskID))
        {
            Debug.LogError("Task ID cannot be null or empty.");
            return;
        }

        if (!Instance.taskEvents.ContainsKey(taskID))
        {
            Instance.taskEvents[taskID] = new UnityEvent();
            Debug.Log($"Registered new event for Task ID: {taskID}");
        }

        Instance.taskEvents[taskID].AddListener(action);
        Debug.Log($"Listener added to Task ID: {taskID}");
    }

    public static void Unsubscribe(string taskID, UnityAction action)
    {
        if (Instance == null || string.IsNullOrEmpty(taskID))
        {
            Debug.LogError("EventCenter not initialized or invalid Task ID.");
            return;
        }

        if (Instance.taskEvents.TryGetValue(taskID, out var unityEvent))
        {
            unityEvent.RemoveListener(action);
            Debug.Log($"Listener removed from Task ID: {taskID}");
        }
    }
}