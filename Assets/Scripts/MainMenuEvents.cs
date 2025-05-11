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

        // 获取所有按钮并注册通用点击事件
        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        // 卸载Start按钮的点击事件
        _startButton.UnregisterCallback<ClickEvent>(OnPlayGameClick);

        // 卸载Continue按钮的点击事件
        _continueButton.UnregisterCallback<ClickEvent>(OnContinueClick);

        // 卸载Quit按钮的点击事件
        _quitButton.UnregisterCallback<ClickEvent>(OnQuitClick);

        // 卸载所有按钮的通用点击事件
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    // Start按钮点击事件
    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Start");
        SceneLoaderManager.Instance.TransitionToScene("Cloud", 1);
        CommandManager.instance.Execute("startdialogue", "-f", "testLoader");
    }

    // Continue按钮点击事件
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

    // Quit按钮点击事件
    private void OnQuitClick(ClickEvent evt)
    {
        Application.Quit();
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}