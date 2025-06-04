using UnityEngine;
using UnityEngine.EventSystems;

public class Mark : MonoBehaviour, IPointerClickHandler
{
    public int theSceneToJumpTo;
    public Vector3 cameraPositon;
    private string transitionStyle = "Cloud";

    private CameraManager cameraManager => CameraManager.instance;

    SceneLoaderManager loaderManager => SceneLoaderManager.Instance;

    public void OnPointerClick(PointerEventData eventData)
    {
        loaderManager.TransitionToScene(transitionStyle, theSceneToJumpTo, 1f, OnSceneTransitionDone);

    }

    private void OnSceneTransitionDone()
    {
        cameraManager.SetCameraPosition(cameraPositon);
    }

    public void UnlockThisMark()
    {
    }
}
