using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayVideo : MonoBehaviour

{
	GameObject[] sceneUI;
	GameObject skipButton;

    VideoPlayer videoPlayer;

    private void Awake()
    {
        sceneUI = UI.Instance.gameUI;
        skipButton = UI.Instance.skipVideoButton;
    }

    public void ExecuteVideo ()
	{
		videoPlayer = this.gameObject.GetComponent<VideoPlayer>();
		videoPlayer.isLooping = false;
		videoPlayer.loopPointReached += EndReached;
		string movieName = videoPlayer.clip.name;

        UI.Instance.EnableGameUI(false);

		videoPlayer.Play();
		#if UNITY_IOS
		SoundManager.MuteMusic(true);
		#endif
		if (!PlayerPrefs.HasKey(movieName))
		{
			skipButton.SetActive(false);
			PlayerPrefs.SetString(movieName, "played");
		}
		else
			skipButton.SetActive (true);	
	}


    private void Start()
    {
#if !UNITY_EDITOR
        if (SceneManager.GetActiveScene ().name == "Level1")
			ExecuteVideo ();
#endif
    }

    private void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        SkipVideo();
    }

    public void SkipVideo()
	{
		videoPlayer.Stop ();
		skipButton.SetActive (false);

        UI.Instance.EnableGameUI(true);
#if UNITY_IOS
        SoundManager.MuteMusic(false);
#endif
		if (SceneManager.GetActiveScene().name == "Level10" || SceneManager.GetActiveScene().name == "Level20")
		{
			UI.Instance.LevelEndUI.SetActive (true);
		}
	}
}
