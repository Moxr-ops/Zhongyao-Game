using UnityEngine;
using UnityEngine.SceneManagement; // ���ڳ�������
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startGameButton;
    public Button continueGameButton;
    public Button exitGameButton;

    // ���� SceneLoad �ű�
    public SceneLoad sceneLoad;

    private void Start()
    {
        // ���ð�ť�ĵ���¼�
        startGameButton.onClick.AddListener(StartGame);
        continueGameButton.onClick.AddListener(ContinueGame);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    // ��ʼ����Ϸ
    public void StartGame()
    {
        // ���� SceneLoad �ķ���������Ϸ����
        sceneLoad.LoadSceneByIndex(1); // ������Ϸ������������ 1
    }

    // ������Ϸ�����ش浵��
    public void ContinueGame()
    {
        // ����Ƿ��д浵
        if (File.Exists(Application.persistentDataPath + "/savegame.dat"))
        {
            // ���ش浵
            GameManager.Instance.LoadGame();

            // ������Ϸ����
            sceneLoad.LoadSceneByIndex(1); // ������Ϸ������������ 1
        }
        else
        {
            // ���û�д浵����ʾ�û�
            Debug.LogWarning("No save file found! Starting new game.");
            StartGame();
        }
    }

    // �˳���Ϸ
    public void ExitGame()
    {
        Application.Quit();
    }
}