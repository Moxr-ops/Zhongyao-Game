using UnityEngine;

public class Player : MonoBehaviour
{
    public int scene; // ��������ĳ�������
    public int[] items; // ���ӵ�е���Ʒ���������飩
    public int storyProgress; // ��������ľ������

    public PlayerData SavePlayerData()
    {
        // ������ҵĵ�ǰ״̬
        return new PlayerData
        {
            scene = scene,
            items = items,
            storyProgress = storyProgress
        };
    }

    public void LoadPlayerData(PlayerData data)
    {
        // ������ҵ�״̬
        scene = data.scene;
        items = data.items;
        storyProgress = data.storyProgress;
    }
}

[System.Serializable]
public class PlayerData
{
    public int scene; // ��������ĳ�������
    public int[] items; // ���ӵ�е���Ʒ
    public int storyProgress; // ��������ľ������
}