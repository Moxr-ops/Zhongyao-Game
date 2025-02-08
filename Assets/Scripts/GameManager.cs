using UnityEngine;
using System.IO; // �����ļ�����
using System.Runtime.Serialization.Formatters.Binary; // ���ڶ��������л�

public class GameManager : MonoBehaviour
{
    public Player player;
    private string saveFilePath;
    public static GameManager Instance;

    private void Awake()
    {
        // ȷ��ֻ��һ�� GameManager ʵ��
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �����л�ʱ������
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // ��ȡ�����ļ���·��
        saveFilePath = Application.persistentDataPath + "/savegame.dat";
    }

    // ������Ϸ
    public void SaveGame()
    {
        // ʹ�� Player �� SavePlayerData ������ȡ�������
        PlayerData playerData = player.SavePlayerData();

        // ʹ�� BinaryFormatter ���������л�Ϊ������
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

    // ������Ϸ
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            // ʹ�� BinaryFormatter ���ļ��з����л�����
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(saveFilePath, FileMode.Open);

            try
            {
                PlayerData playerData = (PlayerData)formatter.Deserialize(fileStream);

                // ʹ�� Player �� LoadPlayerData ���������������
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