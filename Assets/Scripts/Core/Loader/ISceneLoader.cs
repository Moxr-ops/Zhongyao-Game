using DG.Tweening;

public interface ISceneLoader
{
    void Initialize();
    void FadeIn(float duration, Ease ease);
    void FadeOut(float duration, Ease ease);
}