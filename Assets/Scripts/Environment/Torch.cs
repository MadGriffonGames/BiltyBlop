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
    [SerializeField]
    GameObject torchParticle;

    bool isShining;

    BoxCollider2D MyBoxCollider;

	void Start ()
    {
        MyAnimator = GetComponentsInChildren<Animator>();
        MyAnimator[0].enabled = false;
        MyAnimator[1].enabled = false;
        isShining = true;
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
        if (collision.CompareTag("Sword") && isShining)
        {
            light.SetActive(false);
            flame.SetActive(false);
            Instantiate(torchParticle, gameObject.transform.position + new Vector3(0, 0.53f, -1f), Quaternion.Euler(-90,0,0));
            isShining = false;
        }
    }
}
