using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{

    private void LateUpdate()
    {
        if (!this.gameObject.GetComponent<ParticleSystem>().IsAlive())
        {
            Destroy(this.gameObject);
        }
    }

}
