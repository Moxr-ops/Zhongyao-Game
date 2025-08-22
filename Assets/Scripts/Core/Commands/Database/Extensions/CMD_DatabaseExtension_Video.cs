using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using COMMANDS;
using DIALOGUE;
using UnityEngine.UI;

namespace COMMANDS
{
    public class CMD_DatabaseExtension_Video : CMD_DatabaseExtension
    {
        private static string[] PARAM_FADE_DURATION => new string[] { "fd", "fade" };
        private static string[] PARAM_IMMEDIATE => new string[] { "i", "immediate" };

        private static VideoManager videoManager;

        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("playvideo", new Func<string[], IEnumerator>(PlayVideo));
            database.AddCommand("stopvideo", new Func<string[], IEnumerator>(StopVideo));
        }

        private static void InitializeVideoManager()
        {
            if (videoManager == null)
            {
                GameObject videoObj = new GameObject("Video Manager");
                videoManager = videoObj.AddComponent<VideoManager>();

                videoObj.AddComponent<RawImage>();
                videoObj.AddComponent<VideoPlayer>();

                videoManager.videoPlayer = videoObj.GetComponent<VideoPlayer>();
                videoManager.videoPlayer.playOnAwake = false;
                videoManager.videoPlayer.renderMode = VideoRenderMode.RenderTexture;

                videoManager.videoPlayer.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
                videoObj.GetComponent<RawImage>().texture = videoManager.videoPlayer.targetTexture;

                UnityEngine.Object.DontDestroyOnLoad(videoObj);
            }
        }

        private static IEnumerator PlayVideo(string[] data)
        {
            if (data.Length == 0)
            {
                Debug.LogError("PlayVideo command requires a video name!");
                yield break;
            }

            string videoName = data[0];
            float fadeDuration = 1.0f;
            bool immediate = false;

            var parameters = ConvertDataToParameters(data);
            parameters.TryGetValue(PARAM_FADE_DURATION, out fadeDuration, defaultValue: 1.0f);
            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            videoManager.fadeDuration = fadeDuration;

            videoManager.PlayVideo(videoName);

            if (immediate)
                yield break;

            while (!videoManager.videoPlayer.isPrepared || !videoManager.videoPlayer.isPlaying)
                yield return null;
        }

        private static IEnumerator StopVideo(string[] data)
        {
            float fadeDuration = videoManager.fadeDuration;
            bool immediate = false;

            var parameters = ConvertDataToParameters(data);
            parameters.TryGetValue(PARAM_FADE_DURATION, out fadeDuration, defaultValue: fadeDuration);
            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            videoManager.fadeDuration = fadeDuration;

            videoManager.StopVideo();

            if (immediate)
                yield break;

            while (videoManager.IsFading)
                yield return null;
        }
    }
}