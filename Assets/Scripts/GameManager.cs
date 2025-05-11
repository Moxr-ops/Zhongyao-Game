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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}