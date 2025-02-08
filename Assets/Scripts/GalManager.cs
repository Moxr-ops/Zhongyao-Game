using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalManager : MonoBehaviour
{
    public static GalManager instance { get; private set; }
    public GameObject galLoader;
    public Player player;
    public List<ScriptData> scriptDataList; // 包含对话脚本数据的列表

    private int currentEnd = -1;

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
    public int StartGal()
    {
        player.inGal = true;

        // 加载出GalLoader实例
        GameObject gal = Instantiate(galLoader);
        gal.transform.SetParent(transform, false);

        currentEnd = -1;  // 重置当前结束标志

        TalkStream();

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
        // 在这里实现对话显示逻辑，例如：
        // 显示角色名称：script.name
        // 显示角色图片：script.img
        // 显示对话文本：script.txt
        Debug.Log($"Name: {script.name}, Img: {script.img}, Txt: {script.txt}");
    }

    private void EndDialogue()
    {
        player.inGal = false;
        // 其他结束对话的处理，可以是销毁UI元素等
    }
}

public class ScriptData
{
    public string name;
    public string img;
    public string txt;
    public int end = -1;
}