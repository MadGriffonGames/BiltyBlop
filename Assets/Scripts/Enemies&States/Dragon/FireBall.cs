using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    [SerializeField]
    public float speed;

    [SerializeField]
    public GameObject blow;

    Rigidbody2D myRigidbody;

    SpriteRenderer mySR;

    Vector2 direction;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        StartCoroutine(Delay());
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
        if (myRigidbody.bodyType == RigidbodyType2D.Dynamic && transform.localScale.x < 1.5)
        {
            transform.localScale += new Vector3(0.2f, 0.2f, 0);
        }
    }

    public void Initialize(Vector2 dir)
    {
        this.direction = dir;
    }

    IEnumerator Delay()//Function for right time of throwing fireball
    {
        yield return new WaitForSeconds(0.3f);
        myRigidbody.bodyType = RigidbodyType2D.Dynamic;
        mySR.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Instantiate(blow, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            //Instantiate(blow, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
