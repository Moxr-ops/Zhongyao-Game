using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using ARCHIVE;

public class MainMenu : MonoBehaviour
{
    public Button startGameButton;
    public Button continueGameButton;
    public Button exitGameButton;

    public SceneLoad sceneLoad;

    [SerializeField]
    ArchivingManager archivingManager => ArchivingManager.Instance;
    Player player;

    private void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        continueGameButton.onClick.AddListener(ContinueGame);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    public void StartGame()
    {
        sceneLoad.LoadSceneByIndex(1);
    }

    public void ContinueGame()
    {
        if (ArchivingManager.Instance.HaveArchive())
        {
            ArchivingManager.Instance.Load();
            sceneLoad.LoadSceneByIndex(1);
        }
        else
        {
            Debug.LogWarning("No save file found! Starting new game.000");
            StartGame();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}