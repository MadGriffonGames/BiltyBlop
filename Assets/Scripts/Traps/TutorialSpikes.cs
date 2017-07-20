using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpikes : MonoBehaviour
{
    Animator MyAnimator;
    [SerializeField]
    float startDelay;
    [SerializeField]
    float delay;

    void Start()
    {
        MyAnimator = GetComponent<Animator>();
        MyAnimator.enabled = false;
        StartCoroutine(Activate());
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(startDelay);
        SoundManager.PlaySound("trap2");
        MyAnimator.enabled = true;
    }

    IEnumerator Delay()
    {
        MyAnimator.enabled = false;
        yield return new WaitForSeconds(delay);
        MyAnimator.enabled = true;
    }
}
