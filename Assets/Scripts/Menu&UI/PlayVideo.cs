using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour

{

    [SerializeField]
    string movieName;

    private void Start()
    {
		
		this.gameObject.GetComponent<VideoPlayer> ().Play ();
        if (!PlayerPrefs.HasKey(movieName))
        { 
//            Handheld.PlayFullScreenMovie(movieName + ".mp4", Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);

            PlayerPrefs.SetString(movieName, "played");
        }
        else Handheld.PlayFullScreenMovie(movieName + ".mp4", Color.black, FullScreenMovieControlMode.CancelOnInput,FullScreenMovieScalingMode.AspectFit);
    }
}
