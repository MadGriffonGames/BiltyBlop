using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField]
    public Animator[] MyAnimator;
    [SerializeField]
    GameObject torchLight;
    [SerializeField]
    GameObject flame;
    [SerializeField]
    GameObject torchParticle;

    bool isShining;

    BoxCollider2D MyBoxCollider;

	void Start ()
    {
        GameManager.torches++;
        MyAnimator = GetComponentsInChildren<Animator>();

        if (MyAnimator[0] != null)
            MyAnimator[0].enabled = false;

        if (MyAnimator[1] != null)
            MyAnimator[1].enabled = false;

        isShining = true;
    }

    private void OnBecameVisible()
    {
        if (MyAnimator[0] != null)
            MyAnimator[0].enabled = true;

        if (MyAnimator[1] != null)
            MyAnimator[1].enabled = true;
    }

    private void OnBecameInvisible()
    {
        if (MyAnimator[0] != null)
            MyAnimator[0].enabled = false;

        if (MyAnimator[1] != null)
            MyAnimator[1].enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Sword") || collision.CompareTag("Throwing")) && isShining)
        {
            GameManager.torches--;
            torchLight.SetActive(false);
            flame.SetActive(false);
            Instantiate(torchParticle, torchLight.transform.position + new Vector3(0, -0.2f, -1f), Quaternion.Euler(-90,0,0));
            isShining = false;
        }
    }
}
