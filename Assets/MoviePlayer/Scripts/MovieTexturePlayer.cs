using UnityEngine;
using System.Collections;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com

//Note : This script does not support mobile platform
[RequireComponent(typeof(AudioSource))]
public class MovieTexturePlayer : MonoBehaviour
{
		public bool playOnStart = true;
		#if !(UNITY_ANDROID || UNITY_IPHONE) || UNITY_EDITOR
		public MovieTexture movieTexture;
		#endif

		public AudioSource audioSource;

		IEnumerator Start ()
		{
				if (audioSource == null) {
						audioSource = GetComponent<AudioSource> ();
				}
		
				audioSource.Stop ();
				if (playOnStart) {
						Play ();
				}
			yield return 0;
		}
		
		/// <summary>
		/// Play the movie
		/// </summary>
		public void Play ()
		{
			#if !(UNITY_ANDROID || UNITY_IPHONE) || UNITY_EDITOR
			    if(movieTexture == null){
					Debug.Log("Movie texture is undefined");
					return;
		        }
				GetComponent<Renderer>().enabled = true;
				GetComponent<Renderer>().material.mainTexture = movieTexture;
				movieTexture.Play ();
				audioSource.clip = movieTexture.audioClip;
				audioSource.Play ();
			#endif
		}
}