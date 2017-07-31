using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    Light myLight;
    const float DARK_INTENCITY = 0.3f;
    const float LIGHT_INTENCITY = 1.4f;
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
                    if (myLight.intensity == DARK_INTENCITY)
                        lightUp = true;
                }
                else if (myLight.intensity == LIGHT_INTENCITY)
                    lightDown = true;
            }
            else
            {
                if (leaved_x >= extent_x + GetComponent<Collider2D>().bounds.center.x)
                {
                    if (myLight.intensity == LIGHT_INTENCITY)
                        lightDown = true;
                }
                else if (myLight.intensity == DARK_INTENCITY)
                    lightUp = true;
            }
        }
    }

    void LightUp()
    {
        myLight.intensity += 0.01f;
        if (myLight.intensity >= LIGHT_INTENCITY)
        {
            myLight.intensity = LIGHT_INTENCITY;
            lightUp = false;
        }
    }

    void LightDown()
    {
        myLight.intensity -= 0.01f;
        if (myLight.intensity <= DARK_INTENCITY)
        {
            myLight.intensity = DARK_INTENCITY;
            lightDown = false;
        }
    }
}
