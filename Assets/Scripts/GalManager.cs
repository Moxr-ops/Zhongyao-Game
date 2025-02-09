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
    public List<ScriptData> scriptDataList; // �����Ի��ű����ݵ��б�

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

    // ��ʼGalgame�ĶԻ�ģʽ��ֱ��end������-1��������Ҫ���µ���Ʒ��id
    public void StartGal()
    {
        player.inGal = true;

        // ���س�GalLoaderʵ��
        GameObject galLoaderPrefab = Resources.Load<GameObject>("GalLoaderPrefab");
        galLoader = Instantiate(galLoaderPrefab).GetComponent<GalLoader>();

        TalkStream();

        currentEnd = -1;  // ���õ�ǰ������־

    }

    private void TalkStream()
    {
        if (player.storyProgress < scriptDataList.Count)
        {
            ScriptData currentScript = scriptDataList[player.storyProgress];

            // ��ʾ�Ի����ݣ����磬ͨ��UIϵͳ��
            DisplayDialogue(currentScript);

            // ���µ�ǰ������־
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