using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField]
    GameObject platfrom;
    Rigidbody2D MyRigidbody;
    bool shake = false;
    Vector3 startPos;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    private void Update()
    {
        if (shake)
        {
            Vector3 rnd = Random.insideUnitCircle * 0.02f;
            platfrom.transform.localPosition += new Vector3(rnd.x, rnd.y, 0);
        }
    }

    IEnumerator Fall()
    {
        shake = true;
        yield return new WaitForSeconds(0.8f);
        shake = false;
        MyRigidbody.bodyType = RigidbodyType2D.Dynamic;
        MyRigidbody.freezeRotation = true;
        MyRigidbody.gravityScale = 4;
        StartCoroutine(Reset());
        yield return null;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2);
        MyRigidbody.bodyType = RigidbodyType2D.Static;
        transform.position = startPos;
        platfrom.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, false);
        }
    }
}
