using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class GalLoader : MonoBehaviour
{
    public Animator animator;
    public static GalLoader Instance { get; private set; }

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

    public void DestroyGal()
    {
        animator.SetBool("Out", true);
        Destroy(gameObject);
        Instance = null;
    }

    public void Show(ScriptData data)
    {

    }
}
