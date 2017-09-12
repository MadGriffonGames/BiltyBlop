using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreClub : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(1);
        if (other.gameObject.layer == 8)
        {
            CameraEffect.Shake(0.2f, 0.3f);
        }
    }
}
