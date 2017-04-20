using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainTrigger : MonoBehaviour
{
    GameObject rainParticle;

    private void Start()
    {
        rainParticle = GameObject.FindGameObjectWithTag("Rain");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !rainParticle.activeInHierarchy)
        {
            rainParticle.SetActive(true);
        }
        else if (other.gameObject.CompareTag("Player") && rainParticle.activeInHierarchy)
        {
            rainParticle.SetActive(false);
        }
    }
}
