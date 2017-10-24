using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour

{

	[SerializeField]
	GameObject[] sceneUI;
	[SerializeField]
	GameObject skipButton;

	VideoPlayer videoPlayer;

    private void Start()
    {
		videoPlayer = this.gameObject.GetComponent<VideoPlayer> ();
		videoPlayer.isLooping = false;
		videoPlayer.loopPointReached += EndReached;
		string movieName = videoPlayer.clip.name;
		foreach (GameObject sceneui  in sceneUI) 
		{
			sceneui.SetActive (false);			
		}
		videoPlayer.Play ();
		SoundManager.MuteMusic (true);
		if (!PlayerPrefs.HasKey(movieName))
		{
			skipButton.SetActive (false);
			PlayerPrefs.SetString (movieName, "played");
		} 
		else
			skipButton.SetActive (true);
    }

//	private void Update()
//	{
//		if (videoPlayer.loopPointReached) 
//		{
//			SkipVideo ();
//		}
//	}

	private void EndReached(UnityEngine.Video.VideoPlayer vp)
	{
		vp.playbackSpeed = vp.playbackSpeed / 10.0F;
		SkipVideo ();
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
	}
}
