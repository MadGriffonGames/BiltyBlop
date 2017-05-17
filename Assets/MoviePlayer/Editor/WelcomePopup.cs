using UnityEditor;
using UnityEngine;
using IndieStudio.MoviePlayer.Utility;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com

namespace IndieStudio.MoviePlayer.DLEditor
{
		[InitializeOnLoad]
		public class WelcomePopup : EditorWindow
		{
				private static WelcomePopup window;
				private static bool initilized;
				private static bool dontShowWeclomeMessageAgain;
				private static Vector2 size = new Vector2 (550, 380);
				private static string strKey = "SMPEditor_WelcomePopup";

				static WelcomePopup(){
						EditorApplication.update += Update;
				}

				[MenuItem("Tools/Simple Moive Player/Welcome",false,10)]
				static void ReadManual ()
				{
						initilized = false;
						PlayerPrefs.SetInt (strKey, CommonUtil.TrueFalseBoolToZeroOne (false));
						Init ();
				}

				private static void Init(){

						if (initilized) {
								return;
						}

						if(PlayerPrefs.HasKey(strKey)){
								dontShowWeclomeMessageAgain = CommonUtil.ZeroOneToTrueFalseBool(PlayerPrefs.GetInt(strKey));
						}

						if (dontShowWeclomeMessageAgain) {
								return;
						}

						window = (WelcomePopup)EditorWindow.GetWindow (typeof(WelcomePopup));
						window.titleContent.text = "Welcome";
						window.maxSize = size;
						window.maximized = true;
						window.position = new Rect ((Screen.currentResolution.width - size.x)/2, (Screen.currentResolution.height - size.y)/2, size.x,size.y);
						window.Show ();
						window.Focus ();

						initilized = true;

						PlayerPrefs.SetInt (strKey, CommonUtil.TrueFalseBoolToZeroOne (true));
				}

				static void Update ()
				{
						if (Application.isPlaying) {
								if (window != null) {
										window.Close ();
										window = null;
								}
								return;
						}

						if (window == null) {
								Init ();
						}
				}

				void OnGUI ()
				{
						if (window == null) {
								return;
						}

						EditorGUILayout.Separator ();
						EditorGUILayout.LabelField ("Simple Moive Player "+Links.versionCode,EditorStyles.boldLabel);
						EditorGUILayout.Separator ();

						EditorGUILayout.TextArea ("Thank you for downloading Simple Moive Player Package.\n\nIf you have any questions, suggestions, comments , feature requests or bug detected,\ndo not hesitate to Contact US",GUI.skin.label);
						EditorGUILayout.Separator ();

						EditorGUILayout.LabelField ("Setup Quick Time Player (Important Step)",EditorStyles.boldLabel);
						EditorGUILayout.TextArea ("This package requires Quick Time Player, if you do not have it on your computer then :\n\n- Download Quick Time Player and install it\n- Restart your computer\n- Create new unity project\n- Import the Simple Movie Player package",GUI.skin.label);
						EditorGUILayout.Separator ();
						EditorGUILayout.LabelField ("Notes",EditorStyles.boldLabel);
						EditorGUILayout.TextArea ("- Read the Manual in the Docs folder carefully.\n- Use Mobile scene for mobile platform such as Android/IOS.\n- Use Others scene for other supported platforms such as Standalone.\n- Add your target scene to the build settings and then move it to the top.",GUI.skin.label);
						EditorGUILayout.Separator ();
						EditorGUILayout.Separator ();

						EditorGUILayout.BeginHorizontal ();
						GUI.backgroundColor = Colors.yellowColor;
						if (GUILayout.Button ("Read the Manual", GUILayout.Width (120), GUILayout.Height (22))) {
								Application.OpenURL (Links.docPath);
						}
						GUI.backgroundColor = Colors.whiteColor;

						if (GUILayout.Button ("Version Changes", GUILayout.Width (120), GUILayout.Height (22))) {
								Application.OpenURL (Links.versionChangesPath);
						}

						if (GUILayout.Button ("More Assets", GUILayout.Width (120), GUILayout.Height (22))) {
								Application.OpenURL (Links.indieStudioStoreURL);
						}

						if (GUILayout.Button ("Contact US", GUILayout.Width (120), GUILayout.Height (22))) {
								Application.OpenURL (Links.indieStudioContactUsURL);
						}

						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.Separator ();
						EditorGUILayout.Separator ();

						EditorGUILayout.BeginHorizontal ();
						GUILayout.Space (150);
						GUI.backgroundColor = Color.clear;
						if (GUILayout.Button ("Indie Studio", GUILayout.Width (200), GUILayout.Height (22))) {
								Application.OpenURL (Links.indieStudioStoreURL);
						}
						EditorGUILayout.EndHorizontal ();
				}

				void OnInspectorUpdate ()
				{
						Repaint ();
				}
		}
}
