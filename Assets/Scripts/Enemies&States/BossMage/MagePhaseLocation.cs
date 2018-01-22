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
    [SerializeField]
    Transform magePlatform;
    [SerializeField]
    Transform mage;
    bool isPlatformMove = false;
    bool isLavaMove = false;
    bool isCameraMove = false;
    bool isMagePlatformMove = false;
    bool isMageMove = false;
    Vector3 platformsPosition;
    Vector3 lavaPosition;
    Vector3 cameraPosition;
    Vector3 magePosition;
    Vector3 magePlatformPosition;

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

        if (magePlatform.position.y < -8.7f && isMagePlatformMove)
        {
            magePlatformPosition = magePlatform.position;
            magePlatformPosition.y += Time.fixedDeltaTime * 0.8f;
            magePlatform.position = magePlatformPosition;
        }
        else if (isMagePlatformMove)
        {
            magePlatformPosition = magePlatform.position;
            magePlatformPosition.y = -8.7f;
            magePlatform.position = magePlatformPosition;
            isMagePlatformMove = false;
        }

        if (mage.position.y < -5.5f && isMageMove)
        {
            magePosition = mage.position;
            magePosition.y += Time.fixedDeltaTime * 0.8f;
            mage.position = magePosition;
        }
        else if (isMageMove)
        {
            magePosition = mage.position;
            magePosition.y = -5.5f;
            mage.position = magePosition;
            isMagePlatformMove = false;
        }

        if (lavaPosition.y == -13.8f && platformsPosition.y == -13.5f)
        {
            ground.SetActive(false);
            stones.SetActive(false);
        }
    }

    public void OnEnable()
    {
        isCameraMove = true;
        isPlatformMove = true;
        isLavaMove = true;
        isMagePlatformMove = true;
        isMageMove = true;
    }
}
