using UnityEngine;
using DG.Tweening;

public class CloudSceneLoader : MonoBehaviour, ISceneLoader
{
    [Header("ÔÆ¶äÒÆ¶¯²ÎÊý")]
    [SerializeField] private Vector2 cloudStartPos;
    [SerializeField] private Vector2 cloudEndPos;

    private RectTransform[] clouds;

    public void Initialize()
    {
        clouds = new RectTransform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            clouds[i] = transform.GetChild(i) as RectTransform;
            clouds[i].anchoredPosition = cloudStartPos;
        }
    }

    public void FadeIn(float duration, Ease ease)
    {
        foreach (var cloud in clouds)
        {
            cloud.DOAnchorPos(cloudEndPos, duration)
                .SetEase(ease)
                .SetUpdate(true);
        }
    }

    public void FadeOut(float duration, Ease ease)
    {
        foreach (var cloud in clouds)
        {
            cloud.DOAnchorPos(cloudStartPos, duration)
                .SetEase(ease)
                .SetUpdate(true);
        }
    }
}