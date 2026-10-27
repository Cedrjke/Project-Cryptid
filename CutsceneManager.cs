using UnityEngine;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    [Header("Video Setup")]
    public VideoPlayer videoPlayer;
    public VideoClip[] cutsceneClips;

    [Header("Canvases")]
    public GameObject cutsceneCanvas;
    public GameObject mainGameCanvas;

    private int currentCutsceneIndex = 0;

    void Start()
    {
        if (cutsceneCanvas != null) cutsceneCanvas.SetActive(true);
        if (mainGameCanvas != null) mainGameCanvas.SetActive(false);

        if (videoPlayer != null && cutsceneClips.Length > 0)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
            PlayCurrentCutscene();
        }
    }

    void PlayCurrentCutscene()
    {
        if (currentCutsceneIndex < cutsceneClips.Length)
        {
            videoPlayer.clip = cutsceneClips[currentCutsceneIndex];
            videoPlayer.Play();
        }
        else
        {
            FinishCutscenes();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        currentCutsceneIndex++;
        PlayCurrentCutscene();
    }

    public void SkipCutscene()
    {
        videoPlayer.Stop();
        currentCutsceneIndex++;
        PlayCurrentCutscene();
    }

    void FinishCutscenes()
    {
        if (cutsceneCanvas != null) cutsceneCanvas.SetActive(false);
        if (mainGameCanvas != null) mainGameCanvas.SetActive(true);
    }
}