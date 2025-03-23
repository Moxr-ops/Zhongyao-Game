using UnityEngine;
using DG.Tweening;

public class CloudSceneLoader : MonoBehaviour, ISceneLoader
{
    [SerializeField] private float startXOffset;

    private RectTransform _rightCloud;
    private RectTransform _leftCloud;
    private Vector2[] _centerPosition = new Vector2[2];
    private Vector2 _rightStartPos;
    private Vector2 _leftStartPos;

    public void Initialize()
    {
        _rightCloud = transform.Find("R").GetComponent<RectTransform>();
        _leftCloud = transform.Find("L").GetComponent<RectTransform>();

        _rightStartPos = new Vector2(startXOffset, 0);
        _leftStartPos = new Vector2(-startXOffset, 0);

        _centerPosition[0] = new Vector2(0, 0);
        _centerPosition[1] = new Vector2(0, 0);

        ResetClouds();
    }

    public void FadeIn(float duration, Ease ease)
    {
        AnimateClouds(_centerPosition, duration, ease);
    }

    public void FadeOut(float duration, Ease ease)
    {
        AnimateClouds(new Vector2[] { _rightStartPos, _leftStartPos }, duration, ease);
    }

    private void AnimateClouds(Vector2[] targets, float duration, Ease ease)
    {
        _rightCloud.DOAnchorPos(targets[0], duration).SetEase(ease).SetUpdate(true);
        _leftCloud.DOAnchorPos(targets[1], duration).SetEase(ease).SetUpdate(true);
    }

    private void ResetClouds()
    {
        _rightCloud.anchoredPosition = _rightStartPos;
        _leftCloud.anchoredPosition = _leftStartPos;
    }
}