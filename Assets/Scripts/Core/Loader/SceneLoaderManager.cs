using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;

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
        if (!loaders.ContainsKey(loaderName))
        {
            Debug.LogError($"未找到转场控制器: {loaderName}");
            return;
        }

        StartCoroutine(TransitionProcess(loaderName, sceneIndex));
    }

    private IEnumerator TransitionProcess(string loaderName, int sceneIndex)
    {
        ISceneLoader loader = loaders[loaderName];
        GameObject loaderObj = loaderObjects.Find(o => o.name == loaderName);

        loaderObj.SetActive(true);
        loader.FadeIn(defaultFadeDuration, defaultEaseType);
        yield return new WaitForSecondsRealtime(defaultFadeDuration);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        loader.FadeOut(defaultFadeDuration, defaultEaseType);
        yield return new WaitForSecondsRealtime(defaultFadeDuration);
        loaderObj.SetActive(false);
    }
}