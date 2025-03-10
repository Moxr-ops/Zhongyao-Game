using System.Collections;
using System.Collections.Generic;
using ITEMS;
using UnityEngine;
using UnityEngine.Events;

public class TestTask : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Test());
    }
    IEnumerator Test()
    {
        ItemsManager.instance.AddToWarehouse("hdlm");
        Debug.Log(ItemWarehouse.Instance.HasItem("hdlm"));
        yield return new WaitForSeconds(1);
        TaskManager.Instance.CheckTaskProgress();
    }
}