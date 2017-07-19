using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysMovingSpikes : MonoBehaviour
{
    Animator MyAnimator;
    [SerializeField]
    float delay;

	void Start ()
    {
        MyAnimator = GetComponent<Animator>();
        MyAnimator.enabled = false;
    }

	
    private IEnumerator OnBecameVisible()
    {
        yield return new WaitForSeconds(delay);
        SoundManager.PlaySound("trap2");
        MyAnimator.enabled = true;
    }

    private void OnBecameInvisible()
    {
        MyAnimator.enabled = false;
    }
}
