using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Collections;
using DG.Tweening;

public class SceneLoaderManager : MonoBehaviour
{
    public static SceneLoaderManager Instance { get; private set; }

    [Header("全局参数")]
    [SerializeField] private float defaultFadeDuration = 1f;
    [SerializeField] private Ease defaultEaseType = Ease.InOutQuad;

    [Header("转场控制器绑定")]
    [SerializeField] private List<GameObject> loaderObjects;

    private Dictionary<string, ISceneLoader> loaders = new Dictionary<string, ISceneLoader>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeLoaders();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeLoaders()
    {
        foreach (var obj in loaderObjects)
        {
            ISceneLoader loader = obj.GetComponent<ISceneLoader>();
            if (loader != null)
            {
                loaders.Add(obj.name, loader);
                loader.Initialize();
                obj.SetActive(false);
            }
        }
    }

    public void TransitionToScene(string loaderName, int sceneIndex)
    {
        TransitionToScene(loaderName, sceneIndex, defaultFadeDuration, null);
    }

    public void TransitionToScene(string loaderName, int sceneIndex, float fadeDuration, Action onComplete)
    {
        StartCoroutine(TransitionProcess(loaderName, sceneIndex, fadeDuration, onComplete));
    }

    private IEnumerator TransitionProcess(string loaderName, int sceneIndex, float fadeDuration, Action onComplete)
    {
        if (!loaders.ContainsKey(loaderName))
        {
            Debug.LogError($"找不到加载器: {loaderName}");
            yield break;
        }

        ISceneLoader loader = loaders[loaderName];
        GameObject loaderObj = loaderObjects.Find(o => o.name == loaderName);

        loaderObj.SetActive(true);
        loader.FadeIn(fadeDuration, defaultEaseType);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;

        float fadeTimer = 0;
        while (fadeTimer < fadeDuration || asyncLoad.progress < 0.9f)
        {
            fadeTimer += Time.unscaledDeltaTime;
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        onComplete?.Invoke();
        loader.FadeOut(fadeDuration, defaultEaseType);

        yield return new WaitForSecondsRealtime(fadeDuration);
        loaderObj.SetActive(false);
    }
}