using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCUI : MonoBehaviour
{
    public void Return(int sceneIndex)
    {
        SceneLoaderManager.Instance.TransitionToScene("Cloud", sceneIndex);
    }
}
