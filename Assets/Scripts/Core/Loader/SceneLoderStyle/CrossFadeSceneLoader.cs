using UnityEngine;
using DG.Tweening;

public class CrossFadeSceneLoader : MonoBehaviour, ISceneLoader
{
    // [SerializeField] private float fadeDuration = 0.8f; // if wanna change the duration, turn to Manager

    private CanvasGroup _canvasGroup;
    private float _fullAlpha = 1f;
    private float _zeroAlpha = 0f;

    public void Initialize()
    {
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        _canvasGroup.alpha = _zeroAlpha;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void FadeIn(float duration, Ease ease)
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        AnimateFade(_fullAlpha, duration, ease);
    }

    public void FadeOut(float duration, Ease ease)
    {
        _canvasGroup.interactable = false;
        AnimateFade(_zeroAlpha, duration, ease).OnComplete(() =>
            _canvasGroup.blocksRaycasts = false);
    }

    private Tween AnimateFade(float targetAlpha, float duration, Ease ease)
    {
        return _canvasGroup.DOFade(targetAlpha, duration)
            .SetEase(ease)
            .SetUpdate(true);
    }
}