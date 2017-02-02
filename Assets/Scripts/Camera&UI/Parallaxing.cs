using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	[SerializeField]
	private Transform[] backgrounds; // array (list) back- and foregrounds to parallax

	public float[] parallaxScales;   // the proportion of the cameras movement to move the backrounds by

	[SerializeField]
	private float smoothing = 1f; 	// parallaxing amount = how smooth paralax is going to be. must be > 0

	private Transform cam;			// reference to main cameras transform
	private Vector3 previousCamPos; // position of the camera in the previous frame


	// is called before start(). call logic before start. great for writing references
	void Awake ()
	{
		// set up the camera reference
		cam = Camera.main.transform;

	}

	// Use this for initialization
	void Start () 
	{
		// The previous frame had the current frame's camera position
		previousCamPos = cam.position;

		// assigning coresponding parallaxScales
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) 
		{
			parallaxScales [i] = backgrounds [i].position.z * -1;
		}
	}
	
	// Update is called once per frame
	void Update () {

		// for each background
		for (int i = 0; i < backgrounds.Length; i++) 
		{
			// the parallax is the opposite of the camera movement because the previous frame multiplied by scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			// set a target x position wich is the current position plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;
			float backgroundTargetPosY = backgrounds [i].position.y + parallax;

			// create a target position which is the backgrounds current position with it's target x position
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);

			// fade between current pos and target position using lerp
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

		}

		// set previous cam pos to the camera's position at the end of the frame
		previousCamPos = cam.position;
	
	}
}
