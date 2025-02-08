using UnityEngine;
using System.IO; // 用于文件操作
using System.Runtime.Serialization.Formatters.Binary; // 用于二进制序列化

public class GameManager : MonoBehaviour
{
    public Player player;
    private string saveFilePath;
    public static GameManager Instance;

    private void Awake()
    {
        // 确保只有一个 GameManager 实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 场景切换时不销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 获取保存文件的路径
        saveFilePath = Application.persistentDataPath + "/savegame.dat";
    }

    // 保存游戏
    public void SaveGame()
    {
        // 使用 Player 的 SavePlayerData 方法获取玩家数据
        PlayerData playerData = player.SavePlayerData();

        // 使用 BinaryFormatter 将数据序列化为二进制
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create(saveFilePath);

        try
        {
            formatter.Serialize(fileStream, playerData);
            Debug.Log("Game Saved!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving game: " + e.Message);
        }
        finally
        {
            fileStream.Close();
        }
    }

    // 加载游戏
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            // 使用 BinaryFormatter 从文件中反序列化数据
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(saveFilePath, FileMode.Open);

            try
            {
                PlayerData playerData = (PlayerData)formatter.Deserialize(fileStream);

                // 使用 Player 的 LoadPlayerData 方法加载玩家数据
                player.LoadPlayerData(playerData);

                UnityEngine.SceneManagement.SceneManager.LoadScene(player.scene);

                Debug.Log("Game Loaded!");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error loading game: " + e.Message);
            }
            finally
            {
                fileStream.Close();
            }
        }
        else
        {
            Debug.LogError("No save file found!");
        }
    }
}