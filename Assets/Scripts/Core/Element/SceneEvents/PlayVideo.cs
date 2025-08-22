using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using COMMANDS;
using DIALOGUE;

[RequireComponent(typeof(VideoPlayer))]
public class PlayVideo : MonoBehaviour
{
    [Tooltip("视频播放完成后调用的方法")]
    public UnityEngine.Events.UnityEvent onVideoFinished;

    private const string FirstScriptID = "11";

    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        CommandManager.instance.Execute("StopSong");

        videoPlayer.loopPointReached += OnVideoEnd;

        videoPlayer.Play();
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        onVideoFinished?.Invoke();
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }

    public void TurnScene()
    {
        SceneLoaderManager.Instance.TransitionToScene("Cloud", 1, 1.5f, StartEvent);
    }

    private void StartEvent()
    {
        CommandManager.instance.Execute("startdialogue", "-f", FirstScriptID);
    }
}
