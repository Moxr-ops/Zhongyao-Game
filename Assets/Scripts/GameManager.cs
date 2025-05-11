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