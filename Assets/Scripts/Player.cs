using UnityEngine;

public class Player : MonoBehaviour
{
    public int scene; // ��������ĳ�������
    public int[] items; // ���ӵ�е���Ʒ���������飩
    public int storyProgress; // ��������ľ������
    public bool inGal; // ����Ƿ���gal״̬
    public int timesPlayedGame; // ��ҽ�����Ϸ����

    public PlayerData SavePlayerData()
    {
        // ������ҵĵ�ǰ״̬
        return new PlayerData
        {
            scene = scene,
            items = items,
            storyProgress = storyProgress,
            inGal = inGal
        };
    }

    public void LoadPlayerData(PlayerData data)
    {
        // ������ҵ�״̬
        scene = data.scene;
        items = data.items;
        storyProgress = data.storyProgress;
        inGal = data.inGal;
    }
}

[System.Serializable]
public class PlayerData
{
    public int scene; // ��������ĳ�������
    public int[] items; // ���ӵ�е���Ʒ
    public int storyProgress; // ��������ľ������
    public bool inGal; // ����Ƿ���gal״̬
}