using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance {  get; private set; }

    public Camera Camera;

    public Transform targetToFollow;
    public Vector3 offset;
    public float smoothSpeed = 5f;

    private void Awake()
    {
        instance = this;
    }

    public void SetCameraPosition(Vector3 targetPosition)
    {
        if (Camera == null)
        {
            Camera = Camera.main;
        }
        if (Camera != null)
        {
            Camera.transform.position = targetPosition;
        }
    }

    public void MoveCameraToPosition(Vector3 position)
    {
        if (Camera != null)
        {
            Camera.transform.position = position;
        }
    }

    public void FollowTarget()
    {
        if (targetToFollow != null && Camera != null)
        {
            Vector3 desiredPosition = targetToFollow.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(Camera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            Camera.transform.position = smoothedPosition;
        }
    }

    private void LateUpdate()
    {
        if (targetToFollow != null)
        {
            FollowTarget();
        }
    }
}