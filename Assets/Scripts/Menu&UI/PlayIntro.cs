using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIntro : MonoBehaviour
{
    private void Start()
    {
        Handheld.PlayFullScreenMovie("Main_Scene_2.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
    }
}
