using ITEMS;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int scene; // ��������ĳ�������
    public string[] items; // ���ӵ�е���Ʒ���������飩
    public int gameScriptIndex; // ��������ľ������
    public bool inGal; // ����Ƿ���gal״̬
    public int timesPlayedGame; // ��ҽ�����Ϸ����
    public string[] tasks;

    public PlayerData SavePlayerData()
    {
        // ������ҵĵ�ǰ״̬
        return new PlayerData
        {
            scene = SceneManager.GetActiveScene().buildIndex,
            items = ItemWarehouse.Instance.GetAllItems().ToArray(),
            storyProgress = gameScriptIndex,
            inGal = inGal,
            tasks = TaskManager.Instance.GetActiveTaskIDs().ToArray(),
        };
    }

    public void LoadPlayerData(PlayerData data)
    {
        // ������ҵ�״̬
        scene = data.scene;
        items = data.items;
        gameScriptIndex = data.storyProgress;
        inGal = data.inGal;
        tasks = data.tasks;
    }
}

[System.Serializable]
public class PlayerData
{
    public int scene; // ��������ĳ�������
    public string[] items; // ���ӵ�е���Ʒ
    public int storyProgress; // ��������ľ������
    public bool inGal; // ����Ƿ���gal״̬
    public string[] tasks;
}