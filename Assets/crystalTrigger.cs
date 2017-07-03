using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalTrigger : MonoBehaviour
{
    Animator[] myAnimator;

    [SerializeField]
    GameObject[] holes;

    void Start ()
    {
        myAnimator = GetComponentsInChildren<Animator>();
        foreach (GameObject hole in holes)
        {
            hole.SetActive(false);
        }
	}

    public void CrystalAttack()
    {
        StartCoroutine(MakeCrystals());
    }

    IEnumerator MakeCrystals()
    {
        for (int i = 0; i < holes.Length; i++)
        {
            holes[i].SetActive(true);
        }
        for (int i = 0; i < myAnimator.Length; i++)
        {
            myAnimator[i].SetTrigger("triggerOn");
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(0.45f);
        Disable();
    }

    public void Disable()
    {
        for (int i = 0; i < holes.Length; i++)
        {
            holes[i].SetActive(false);
        }
    }
}
