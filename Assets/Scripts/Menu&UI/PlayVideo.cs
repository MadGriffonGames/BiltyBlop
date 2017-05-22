using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideo : MonoBehaviour

{

    [SerializeField]
    string movieName;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(movieName))
        { 
            Handheld.PlayFullScreenMovie(movieName + ".mp4", Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
            PlayerPrefs.SetString(movieName, "played");
        } else Handheld.PlayFullScreenMovie(movieName + ".mp4", Color.black, FullScreenMovieControlMode.CancelOnInput,FullScreenMovieScalingMode.AspectFit);
    }
}
