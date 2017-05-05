using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainTrigger : MonoBehaviour
{
    GameObject rainParticle;

    [SerializeField]
    private bool isExit;

    private void Start()
    {
        rainParticle = GameObject.FindGameObjectWithTag("Rain");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float leaved_x = other.transform.position.x;
            float extent_x = GetComponent<Collider2D>().bounds.extents.x;
            if (isExit)
            {
               if(leaved_x>=extent_x+GetComponent<Collider2D>().bounds.center.x)
                {
                    if (!rainParticle.activeInHierarchy)
                        rainParticle.SetActive(true);
                }
               else
                    if (rainParticle.activeInHierarchy)
                    rainParticle.SetActive(false);
            }
            else
            {
                if (leaved_x >= extent_x + GetComponent<Collider2D>().bounds.center.x)
                {
                    if (rainParticle.activeInHierarchy)
                        rainParticle.SetActive(false);
                }
                else
                    if (!rainParticle.activeInHierarchy)
                    rainParticle.SetActive(true);
            }
        }
    }
}
