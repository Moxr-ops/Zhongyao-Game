using UnityEngine;

public class Player : MonoBehaviour
{
    public int scene; // 玩家所处的场景索引
    public int[] items; // 玩家拥有的物品（整型数组）
    public int storyProgress; // 玩家所处的剧情进度

    public PlayerData SavePlayerData()
    {
        // 返回玩家的当前状态
        return new PlayerData
        {
            scene = scene,
            items = items,
            storyProgress = storyProgress
        };
    }

    public void LoadPlayerData(PlayerData data)
    {
        // 加载玩家的状态
        scene = data.scene;
        items = data.items;
        storyProgress = data.storyProgress;
    }
}

[System.Serializable]
public class PlayerData
{
    public int scene; // 玩家所处的场景索引
    public int[] items; // 玩家拥有的物品
    public int storyProgress; // 玩家所处的剧情进度
}