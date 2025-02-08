using UnityEngine;
using UnityEngine.SceneManagement; // 用于场景管理
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startGameButton;
    public Button continueGameButton;
    public Button exitGameButton;

    // 引用 SceneLoad 脚本
    public SceneLoad sceneLoad;

    private void Start()
    {
        // 设置按钮的点击事件
        startGameButton.onClick.AddListener(StartGame);
        continueGameButton.onClick.AddListener(ContinueGame);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    // 开始新游戏
    public void StartGame()
    {
        // 调用 SceneLoad 的方法加载游戏场景
        sceneLoad.LoadSceneByIndex(1); // 假设游戏场景的索引是 1
    }

    // 继续游戏（加载存档）
    public void ContinueGame()
    {
        // 检查是否有存档
        if (File.Exists(Application.persistentDataPath + "/savegame.dat"))
        {
            // 加载存档
            GameManager.Instance.LoadGame();

            // 加载游戏场景
            sceneLoad.LoadSceneByIndex(1); // 假设游戏场景的索引是 1
        }
        else
        {
            // 如果没有存档，提示用户
            Debug.LogWarning("No save file found! Starting new game.");
            StartGame();
        }
    }

    // 退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }
}