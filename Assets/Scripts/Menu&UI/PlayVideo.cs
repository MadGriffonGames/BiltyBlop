using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayVideo : MonoBehaviour

{

	[SerializeField]
	GameObject[] sceneUI;
	[SerializeField]
	GameObject skipButton;

    VideoPlayer videoPlayer;

	public void executeVideo ()
	{
		videoPlayer = this.gameObject.GetComponent<VideoPlayer>();
		videoPlayer.isLooping = false;
		videoPlayer.loopPointReached += EndReached;
		string movieName = videoPlayer.clip.name;
		foreach (GameObject sceneui in sceneUI)
		{
			sceneui.SetActive(false);
		}
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
			executeVideo ();
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
		foreach (GameObject sceneui  in sceneUI) 
		{
			sceneui.SetActive (true);			
		}
		SoundManager.MuteMusic (false);
		if (SceneManager.GetActiveScene().name == "Level10" || SceneManager.GetActiveScene().name == "Level20")
		{
			UI.Instance.LevelEndUI.SetActive (true);
		}
	}
}
