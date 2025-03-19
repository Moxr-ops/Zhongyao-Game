using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startGameButton;
    public Button continueGameButton;
    public Button exitGameButton;

    public SceneLoad sceneLoad;

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
        if (File.Exists(Application.persistentDataPath + "/savegame.dat"))
        {
            GameManager.Instance.LoadGame();

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