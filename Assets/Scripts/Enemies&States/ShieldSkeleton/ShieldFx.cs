using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFx : MonoBehaviour
{
    Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        SoundManager.PlaySound("metall_shield_sound");
        myAnimator.enabled = true;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
