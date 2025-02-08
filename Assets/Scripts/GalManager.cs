using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalManager : MonoBehaviour
{
    public static GalManager instance { get; private set; }
    public GameObject galLoader;
    public Player player;
    public List<ScriptData> scriptDataList; // �����Ի��ű����ݵ��б�

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

    // ��ʼGalgame�ĶԻ�ģʽ��ֱ��end������-1��������Ҫ���µ���Ʒ��id
    public int StartGal()
    {
        player.inGal = true;

        // ���س�GalLoaderʵ��
        GameObject gal = Instantiate(galLoader);
        gal.transform.SetParent(transform, false);

        currentEnd = -1;  // ���õ�ǰ������־

        TalkStream();

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
        // ������ʵ�ֶԻ���ʾ�߼������磺
        // ��ʾ��ɫ���ƣ�script.name
        // ��ʾ��ɫͼƬ��script.img
        // ��ʾ�Ի��ı���script.txt
        Debug.Log($"Name: {script.name}, Img: {script.img}, Txt: {script.txt}");
    }

    private void EndDialogue()
    {
        player.inGal = false;
        // ���������Ի��Ĵ�������������UIԪ�ص�
    }
}

public class ScriptData
{
    public string name;
    public string img;
    public string txt;
    public int end = -1;
}