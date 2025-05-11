using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using DIALOGUE;
using ARCHIVE;

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

        _startButton = _document.rootVisualElement.Q("Start") as Button;
        _startButton.RegisterCallback<ClickEvent>(OnPlayGameClick);

        _continueButton = _document.rootVisualElement.Q("Continue") as Button;
        _continueButton.RegisterCallback<ClickEvent>(OnContinueClick);

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
        CommandManager.instance.Execute("startdialogue", "-f", "testLoader");
    }

    // Continue��ť����¼�
    private void OnContinueClick(ClickEvent evt)
    {
        if (ArchivingManager.Instance.HaveArchive())
        {
            ArchivingManager.Instance.Load();
        }
        else
        {
            Debug.Log("No save file found! Starting new game.");
            SceneLoaderManager.Instance.TransitionToScene("Cloud", 1);
        }
    }

    // Quit��ť����¼�
    private void OnQuitClick(ClickEvent evt)
    {
        Application.Quit();
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}