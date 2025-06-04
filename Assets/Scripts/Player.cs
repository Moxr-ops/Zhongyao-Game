using ITEMS;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int scene; // 玩家所处的场景索引
    public string[] items; // 玩家拥有的物品（整型数组）
    public int gameScriptIndex; // 玩家所处的剧情进度
    public bool inGal; // 玩家是否处于gal状态
    public int timesPlayedGame; // 玩家进入游戏次数
    public string[] tasks;

    public PlayerData SavePlayerData()
    {
        // 返回玩家的当前状态
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
        // 加载玩家的状态
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
    public int scene; // 玩家所处的场景索引
    public string[] items; // 玩家拥有的物品
    public int storyProgress; // 玩家所处的剧情进度
    public bool inGal; // 玩家是否处于gal状态
    public string[] tasks;
}