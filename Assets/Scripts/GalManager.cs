using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class GalManager : MonoBehaviour
{
    public static GalManager instance { get; private set; }
    private GalLoader galLoader;
    public Player player;
    public List<ScriptData> scriptDataList; // 包含对话脚本数据的列表

    public int currentEnd = -1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (player.inGal && currentEnd == -1 && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            NextTalk();
        }
    }

    // 开始Galgame的对话模式，直到end不等于-1，返回需要更新的物品的id
    public void StartGal()
    {
        player.inGal = true;

        // 加载出GalLoader实例
        GameObject galLoaderPrefab = Resources.Load<GameObject>("GalLoaderPrefab");
        galLoader = Instantiate(galLoaderPrefab).GetComponent<GalLoader>();

        TalkStream();

        currentEnd = -1;  // 重置当前结束标志

    }

    private void TalkStream()
    {
        if (player.storyProgress < scriptDataList.Count)
        {
            ScriptData currentScript = scriptDataList[player.storyProgress];

            // 显示对话内容（例如，通过UI系统）
            DisplayDialogue(currentScript);

            // 更新当前结束标志
            currentEnd = currentScript.end;
        }
        else
        {
            EndDialogue();
        }
    }

    public void NextTalk()
    {
        if (currentEnd == -1 && player.storyProgress < scriptDataList.Count - 1)
        {
            player.storyProgress++;
            TalkStream();
        }
        else
        {
            EndDialogue();
        }
    }

    private void DisplayDialogue(ScriptData script)
    {
        galLoader.Show(script);
        Debug.Log($"Name: {script.name}, Img: {script.img}, Txt: {script.txt}");
    }

    private void EndDialogue()
    {
        if (GalLoader.Instance != null)
        {
            GalLoader.Instance.DestroyGal();
        }
    }
}

public class ScriptData
{
    public string name;
    public string img;
    public string txt;
    public int end = -1;
}