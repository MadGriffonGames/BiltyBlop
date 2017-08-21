using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStone : MonoBehaviour {


    /*[SerializeField]
    public GameObject particle;*/

    private Rigidbody2D myRigidbody;

    private Vector2 direction;
    [SerializeField]
    GameObject particle;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("DestroyGround"))
        {
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            this.gameObject.SetActive(false);
            SoundManager.PlaySound("stone_crash_new");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.PlaySound("stone_crash_new");
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0, -0.7f, 0), Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }

    public void Throw(Vector3 startPos)
    {
        this.transform.position = startPos;
        gameObject.SetActive(true);
    }
}
