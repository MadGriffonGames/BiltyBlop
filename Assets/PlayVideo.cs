using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]

public class PlayVideo : MonoBehaviour {

	public MovieTexture movie;
	private AudioSource audio;

	// Use this for initialization
	void Start () {

		GetComponent<RawImage> ().texture = movie as MovieTexture;
		audio = GetComponent<AudioSource> ();
		audio.clip = movie.audioClip;
		movie.Play ();
		audio.Play ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
