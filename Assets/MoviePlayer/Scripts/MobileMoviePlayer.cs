using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com

public class MobileMoviePlayer : MonoBehaviour
{
		public string movieFileName;
		public Color backgroundColor = Color.black;
		public Text messsage;

		#if UNITY_ANDROID || UNITY_IPHONE
		public FullScreenMovieControlMode controlMod = FullScreenMovieControlMode.Hidden;
		public FullScreenMovieScalingMode scalingMod = FullScreenMovieScalingMode.Fill;
		#endif

		public bool playOnStart = true;

		IEnumerator Start ()
		{
				#if !UNITY_EDITOR
					if(messsage!=null){
						messsage.enabled = false;
					}
				#endif

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
				if (string.IsNullOrEmpty (movieFileName)) {
						Debug.Log("movieFileName is undefined");
						return;
				}

				#if UNITY_ANDROID || UNITY_IPHONE
					//Play full screen only
					Handheld.PlayFullScreenMovie (movieFileName,  backgroundColor, controlMod,scalingMod);
				#endif
		}
}