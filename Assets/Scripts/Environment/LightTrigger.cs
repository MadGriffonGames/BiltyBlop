using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    Light myLight;
    float darkIntencity = 0.3f;
    float lightIntencity = 1.4f;
    bool lightUp = false;
    bool lightDown = false;

    [SerializeField]
    private bool isExit;

    private void Start()
    {
        myLight = FindObjectOfType<Light>();
    }


    private void Update()
    {
        if (lightUp)
        {
            LightUp();
        }
        if (lightDown)
        {
            LightDown();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float leaved_x = other.transform.position.x;
            float extent_x = GetComponent<Collider2D>().bounds.extents.x;
            if (isExit)
            {
                if (leaved_x >= extent_x + GetComponent<Collider2D>().bounds.center.x)
                {
                    if (myLight.intensity == darkIntencity)
                        lightUp = true;
                }
                else if (myLight.intensity == lightIntencity)
                    lightDown = true;
            }
            else
            {
                if (leaved_x >= extent_x + GetComponent<Collider2D>().bounds.center.x)
                {
                    if (myLight.intensity == lightIntencity)
                        lightDown = true;
                }
                else if (myLight.intensity == darkIntencity)
                    lightUp = true;
            }
        }
    }

    void LightUp()
    {
        myLight.intensity += 0.01f;
        if (myLight.intensity >= lightIntencity)
        {
            myLight.intensity = lightIntencity;
            lightUp = false;
        }
    }

    void LightDown()
    {
        myLight.intensity -= 0.01f;
        if (myLight.intensity <= darkIntencity)
        {
            myLight.intensity = darkIntencity;
            lightDown = false;
        }
    }
}
