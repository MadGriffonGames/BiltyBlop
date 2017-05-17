using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{

    public static float zoomSpeed;
    public static float targetOrtho;
    public static float smoothSpeed;
    public static float minOrtho;
    public static float maxOrtho = 20.0f;
    public static bool zoomIsOn = false;

    void Start()
    {
        targetOrtho = Camera.main.orthographicSize;
    }

    public static void makeZoom(float speed, float smooth, float maxZoom)
    {
        zoomIsOn = true;
        zoomSpeed = speed;
        smoothSpeed = smooth;
        minOrtho = maxZoom;
    }


    public static void stopZoom()
    {
        zoomIsOn = false;
    }

    void Update()
    {
        if (zoomIsOn)
        {
            targetOrtho -=  zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
			Camera.main.transform.position = Vector3.Slerp (Camera.main.transform.position, Player.Instance.target.transform.position - new Vector3(0,0,15), 0.2f);
			Camera.main.gameObject.GetComponent<FollowCamera> ().enabled = false;
        }


    }
}
