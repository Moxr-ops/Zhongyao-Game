using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;
using ITEMS;

public class EventCenter : MonoBehaviour
{
    public static EventCenter Instance;

    public Dictionary<string, UnityEvent> taskEvents = new();

    void Awake() => Instance = this;

    public static void Subscribe(string taskID, UnityAction action)
    {
        if (!Instance.taskEvents.ContainsKey(taskID))
        {
            Instance.taskEvents[taskID] = new UnityEvent();
        }
        Instance.taskEvents[taskID].AddListener(action);
    }
}
