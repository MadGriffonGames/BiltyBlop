using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOutro : MonoBehaviour
{
    private void Start()
    {
        Handheld.PlayFullScreenMovie("After_Dragon.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
    }
}
