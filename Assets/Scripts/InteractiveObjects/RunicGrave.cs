using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunicGrave : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject particle;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Sword"))
        {
            CameraEffect.Shake(0.2f, 0.1f);
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            SoundManager.PlaySound("door_explode");
            Destroy(this.gameObject);
        }

        if (other.gameObject.layer == 8)
        {
            Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
        }
    }


}
