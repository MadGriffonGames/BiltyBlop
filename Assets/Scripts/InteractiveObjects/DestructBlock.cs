using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructBlock : MonoBehaviour
{
    [SerializeField]
    GameObject particle;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Sword"))
        {
            CameraEffect.Shake(0.2f, 0.1f);
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            SoundManager.PlaySound("wooden_box");
            Destroy(this.gameObject);
        }
    }
}
