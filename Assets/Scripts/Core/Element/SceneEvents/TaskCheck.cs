using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCheck : MonoBehaviour
{
    void Start()
    {
        TaskManager.Instance.CheckTaskProgress();
    }
}
