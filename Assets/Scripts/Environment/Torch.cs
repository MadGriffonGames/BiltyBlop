using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField]
    public Animator[] MyAnimator;
    [SerializeField]
    GameObject light;
    [SerializeField]
    GameObject flame;

    Rigidbody2D MyRigidbody;

	void Start ()
    {
        MyAnimator = GetComponentsInChildren<Animator>();
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyAnimator[0].enabled = false;
        MyAnimator[1].enabled = false;
    }

    private void OnBecameVisible()
    {
        MyAnimator[0].enabled = true;
        MyAnimator[1].enabled = true;
    }

    private void OnBecameInvisible()
    {
        MyAnimator[0].enabled = false;
        MyAnimator[1].enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            light.SetActive(false);
            flame.SetActive(false);
            MyRigidbody.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
}
