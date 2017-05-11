using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEdge : MonoBehaviour
{
    private void Start()
    {

    }
    public void StartDraw(float drawDur, float freezeDur)
    {
        GameObject child = new GameObject("SwordSlash");
        child.transform.parent = this.gameObject.transform;
        child.transform.localPosition = new Vector3(0f, 0f, -5f);
        SwordSlash ssl = child.AddComponent<SwordSlash>();
        StartCoroutine( ssl.StartDraw(drawDur, freezeDur));
    }

}
