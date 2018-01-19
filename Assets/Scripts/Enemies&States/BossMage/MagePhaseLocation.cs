using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagePhaseLocation : MonoBehaviour
{
    [SerializeField]
    GameObject ground;
    [SerializeField]
    GameObject stones;
    [SerializeField]
    Transform platforms;
    [SerializeField]
    Transform lava;
    [SerializeField]
    Transform cam;
    public bool isPlatformMove = false;
    public bool isLavaMove = false;
    public bool isCameraMove = false;
    Vector3 platformsPosition;
    Vector3 lavaPosition;
    Vector3 cameraPosition;

    private void FixedUpdate()
    {
        if (platforms.position.y > -13.5f && isPlatformMove)
        {
            platformsPosition = platforms.position;
            platformsPosition.y -= Time.fixedDeltaTime * 5;
            platforms.position = platformsPosition;
        }
        else if(isPlatformMove)
        {
            platformsPosition = platforms.position;
            platformsPosition.y = -13.5f;
            platforms.position = platformsPosition;
            isPlatformMove = false;
        }

        if (lava.position.y < -13.8f && isLavaMove)
        {
            lavaPosition = lava.position;
            lavaPosition.y += Time.fixedDeltaTime * 1.1f;
            lava.position = lavaPosition;
        }
        else if (isLavaMove)
        {
            lavaPosition = lava.position;
            lavaPosition.y = -13.8f;
            lava.position = lavaPosition;
            isLavaMove = false;
        }

        if (cam.position.y < -10.9f && isCameraMove)
        {
            cameraPosition = cam.position;
            cameraPosition.y += Time.fixedDeltaTime * 0.7f;
            cam.position = cameraPosition;
        }
        else if (isCameraMove)
        {
            cameraPosition = cam.position;
            cameraPosition.y = -10.9f;
            cam.position = cameraPosition;
            isCameraMove = false;
        }

        if (lavaPosition.y == -13.8f && platformsPosition.y == -13.5f)
        {
            ground.SetActive(false);
            stones.SetActive(false);
        }
    }
}
