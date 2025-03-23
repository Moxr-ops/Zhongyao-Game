using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public class MainMenuEvents : MonoBehaviour
{
    public SceneLoad sceneLoad;
    public Player player;

    private UIDocument _document;

    private Button _startButton;
    private Button _continueButton;
    private Button _quitButton;

    private List<Button> _menuButtons = new List<Button>();

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        // Start��ť
        _startButton = _document.rootVisualElement.Q("Start") as Button;
        _startButton.RegisterCallback<ClickEvent>(OnPlayGameClick);

        // Continue��ť
        _continueButton = _document.rootVisualElement.Q("Continue") as Button;
        _continueButton.RegisterCallback<ClickEvent>(OnContinueClick);

        // Quit��ť
        _quitButton = _document.rootVisualElement.Q("Quit") as Button;
        _quitButton.RegisterCallback<ClickEvent>(OnQuitClick);

        // ��ȡ���а�ť��ע��ͨ�õ���¼�
        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        // ж��Start��ť�ĵ���¼�
        _startButton.UnregisterCallback<ClickEvent>(OnPlayGameClick);

        // ж��Continue��ť�ĵ���¼�
        _continueButton.UnregisterCallback<ClickEvent>(OnContinueClick);

        // ж��Quit��ť�ĵ���¼�
        _quitButton.UnregisterCallback<ClickEvent>(OnQuitClick);

        // ж�����а�ť��ͨ�õ���¼�
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    // Start��ť����¼�
    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Start");
        SceneLoaderManager.Instance.TransitionToScene("Cloud", 1);
    }

    // Continue��ť����¼�
    private void OnContinueClick(ClickEvent evt)
    {
        if (File.Exists(Application.persistentDataPath + "/savegame.dat"))
        {

            GameManager.Instance.LoadGame();

            sceneLoad.LoadSceneByIndex(player.scene); // ���ش浵���߼���������
        }
        else
        {
            // ���û�д浵����ʾ�û�
            Debug.LogWarning("No save file found! Starting new game.");
            sceneLoad.LoadSceneByIndex(1);
        }
    }

    // Quit��ť����¼�
    private void OnQuitClick(ClickEvent evt)
    {
        Application.Quit();
    }

    // ���а�ť��ͨ�õ���¼�
    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}