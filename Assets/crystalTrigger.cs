using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalTrigger : MonoBehaviour {

    Animator[] myAnimator;
    bool isTriggered = false;
    [SerializeField]
    GameObject[] holes;

    // Use this for initialization
    void Start ()
    {
        myAnimator = GetComponentsInChildren<Animator>();
        foreach (GameObject item in holes)
        {
            item.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < holes.Length; i++)
            {
                holes[i].SetActive(true);
            }
            StartCoroutine(crystallize());
           // yield return new WaitForSeconds(1.8f);
            //StartCoroutine(crystallizeOff());
        }
    }

    IEnumerator crystallize()
    {
        for (int i = 0; i < myAnimator.Length; i++)
        {
            myAnimator[i].SetTrigger("triggerOn");
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < holes.Length; i++)
        {
            Destroy(holes[i]);
            Destroy(this);
        }

    }

    IEnumerator crystallizeOff()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < myAnimator.Length; i++)
        {
            myAnimator[i].SetTrigger("triggerOff");
            
        }
    }
}
