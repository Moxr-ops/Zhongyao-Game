using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScene : MonoBehaviour
{
    void Start()
    {
        SceneLoaderManager.Instance.TransitionToScene("Cloud", 1);
    }
}
