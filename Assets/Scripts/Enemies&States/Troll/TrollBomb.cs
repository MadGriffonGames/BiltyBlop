using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollBomb : MonoBehaviour
{
    [SerializeField]
    public GameObject blow;

    [SerializeField]
    public float blowTime;

    Rigidbody2D myRigidbody;

    float timer;

	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= blowTime)
        {
            timer = 0;

            GameObject tmp = Instantiate(blow, transform.position, Quaternion.identity);
            tmp.transform.parent = null;

            Destroy(this.gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject tmp = Instantiate(blow, transform.position, Quaternion.identity);
            tmp.transform.parent = null;

            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.collider, true);
        }
    }
}
