using System.Collections;
using System.Collections.Generic;
using ARCHIVE;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class SceneAMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private string transationStyle;
    
    public void OpenMenuPanel()
    {
        menuPanel.SetActive(true);
    }
    public void CloseMenuPanel()
    {
        menuPanel.SetActive(false);
    }

    public void TurnToMainMenu()
    {
        SceneLoaderManager.Instance.TransitionToScene(transationStyle, 0);

        ArchivingManager.Instance.Save();
    }

    public void TurnToMountain()
    {
        SceneLoaderManager.Instance.TransitionToScene(transationStyle, 2);

        ArchivingManager.Instance.Save();
    }
}
