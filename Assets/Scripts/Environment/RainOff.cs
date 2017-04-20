using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainOff : MonoBehaviour
{
    GameObject rainParticle;

    private void Start()
    {
        rainParticle = GameObject.FindGameObjectWithTag("Rain");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rainParticle.transform.localPosition += new Vector3(0, 0, -1);
        }
    }
}
