using UnityEngine;

public class Player : MonoBehaviour
{
    public int scene; // 玩家所处的场景索引
    public int[] items; // 玩家拥有的物品（整型数组）
    public int storyProgress; // 玩家所处的剧情进度
    public bool inGal; // 玩家是否处于gal状态
    public int timesPlayedGame; // 玩家进入游戏次数

    public PlayerData SavePlayerData()
    {
        // 返回玩家的当前状态
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
        // 加载玩家的状态
        scene = data.scene;
        items = data.items;
        storyProgress = data.storyProgress;
        inGal = data.inGal;
    }
}

[System.Serializable]
public class PlayerData
{
    public int scene; // 玩家所处的场景索引
    public int[] items; // 玩家拥有的物品
    public int storyProgress; // 玩家所处的剧情进度
    public bool inGal; // 玩家是否处于gal状态
}