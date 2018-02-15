using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    [SerializeField]
    Collider2D damageTrigger;
    Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Delay());
            StartCoroutine(PlayTrapSound());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        myAnimator.enabled = true;
    }

    public void EnableCollider()//call from animator
    {
        damageTrigger.enabled = true;
    }

    public void DisableCollider()//call from animator
    {
        damageTrigger.enabled = false;
    }

    IEnumerator PlayTrapSound()
    {
        yield return new WaitForSeconds(0.55f);
        SoundManager.PlaySound("bear_trap_sound");
    }
}
