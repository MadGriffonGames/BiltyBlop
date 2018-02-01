using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolemSpikes : MonoBehaviour
{
    Transform parent;
    Vector3 startPos;

    private void Start()
    {
        parent = transform.parent;
        startPos = transform.localPosition;
    }

    public void SetSpikesParent()
    {
        transform.parent = parent;
        transform.localPosition = startPos;
    }

    public void UnsetSpikesParent()
    {
        transform.parent = null;
    }

    public void EndAnim()
    {
        GetComponent<Animator>().enabled = false;
        this.gameObject.SetActive(false);
    }
}
