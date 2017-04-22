using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysMovingSpikes : MonoBehaviour
{
    Animator MyAnimator;
    [SerializeField]
    float delay;
	// Use this for initialization
	void Start () {
        MyAnimator = GetComponent<Animator>();
        MyAnimator.enabled = false;
    }

	
    private IEnumerator OnBecameVisible()
    {
        yield return new WaitForSeconds(delay);
        MyAnimator.enabled = true;
    }

    private void OnBecameInvisible()
    {
        MyAnimator.enabled = false;
    
    }
}
