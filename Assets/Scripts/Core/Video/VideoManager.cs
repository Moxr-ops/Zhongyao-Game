using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    public static VideoManager instance {  get; private set; }

    [Header("Video Settings")]
    public VideoPlayer videoPlayer;
    public float fadeDuration = 1.0f;

    public bool IsFading => isFading;
    public bool IsPlaying => videoPlayer.isPlaying;

    private RawImage rawImage;
    private bool isFading = false;
    private float fadeTimer = 0f;
    private bool autoCloseOnComplete = false;

    void Awake()
    {
        instance = this;

        if (videoPlayer.targetTexture == null)
            videoPlayer.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

        //rawImage.texture = videoPlayer.targetTexture;

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
            videoPlayer.loopPointReached -= OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        if (autoCloseOnComplete)
            StopVideo();
    }

    public void PlayVideo(string videoName)
    {
        PlayVideoInternal(videoName, false);
    }

    public void PlayVideoAndAutoClose(string videoName)
    {
        PlayVideoInternal(videoName, true);
    }

    private void PlayVideoInternal(string videoName, bool autoClose)
    {
        if (isFading) return;

        autoCloseOnComplete = autoClose;

        VideoClip clip = Resources.Load<VideoClip>("Video/" + videoName);
        if (clip == null)
        {
            Debug.LogError($"Video not found: {videoName}");
            return;
        }

        //rawImage.enabled = true;
        //rawImage.color = Color.white;
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        if (!rawImage.enabled) return;

        if (videoPlayer.isPlaying && !isFading)
        {
            isFading = true;
            fadeTimer = fadeDuration;
        }
        else if (!videoPlayer.isPlaying)
        {
            rawImage.enabled = false;
        }
    }

    void Update()
    {
        HandleFade();
    }

    private void HandleFade()
    {
        if (!isFading) return;

        fadeTimer -= Time.deltaTime;
        if (fadeTimer <= 0)
        {
            FinalizeStop();
        }
        else
        {
            float alpha = fadeTimer / fadeDuration;
            rawImage.color = new Color(1, 1, 1, alpha);
        }
    }

    private void FinalizeStop()
    {
        videoPlayer.Stop();
        rawImage.enabled = false;
        rawImage.color = Color.white;
        isFading = false;
        autoCloseOnComplete = false;
    }
}