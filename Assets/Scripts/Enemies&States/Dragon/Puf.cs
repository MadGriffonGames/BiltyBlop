using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puf : MonoBehaviour
{
    Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        myAnimator.Play("Puf");
    }
}
