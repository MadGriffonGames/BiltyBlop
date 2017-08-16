using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDisappearingPlatform : MonoBehaviour
{
    [SerializeField]
    GameObject platfrom;
    [SerializeField]
    float shakingTime;
    Rigidbody2D MyRigidbody;
    bool shake = false;
    Vector3 startPos;
    [SerializeField]
    GameObject groundCollider;
    float lifeTimer;
    float dropTime = 3.2f;

    void Start()
    {
        lifeTimer = Time.time;
        MyRigidbody = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        startPos = transform.position;
    }

    private void Update()
    {
        if (shake)
        {
            Vector3 rnd = Random.insideUnitCircle * 0.02f;
            platfrom.transform.localPosition += new Vector3(rnd.x, rnd.y, 0);
        }

        if (Time.time - lifeTimer > dropTime)
        {
            StartCoroutine(Fall(shakingTime));
        }
    }

    IEnumerator Fall(float time)
    {
        shake = true;
        yield return new WaitForSeconds(time);
        shake = false;
        groundCollider.gameObject.SetActive(false);
        MyRigidbody.bodyType = RigidbodyType2D.Dynamic;
        MyRigidbody.freezeRotation = true;
        MyRigidbody.gravityScale = 4;
        Destroy(this);
        //StartCoroutine(Reset());
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
            StartCoroutine(Fall(shakingTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), false);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), false);
        }
    }
}
