using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchBird : MonoBehaviour {
    public Transform target;
    Animator myAnimator;
    Vector3 nextPos;
    int nextPosNum = 1;
    [SerializeField]
    GameObject shadow;
    /*[SerializeField]
    public UnityEngine.Transform[] pathPoints;*/
    float deltaHigh = -2;

    public Vector3[] pathCordinates = new Vector3[4];

    // Use this for initialization
    void Start () {
        myAnimator = GetComponent<Animator>();
        myAnimator.enabled = false;
        //pathCordinates = new Vector3[pathPoints.Length];
        int i = 0;
        foreach(var point in pathCordinates)
        {
            Vector3 tmp = new Vector3(-Player.Instance.transform.localPosition.x+i, Player.Instance.transform.localPosition.y+1.5f, Player.Instance.transform.localPosition.z);
            pathCordinates[i] = tmp;
            i++;
        }
    }
	
	// Update is called once per frame


	void FixedUpdate () {
        {
            nextPos = pathCordinates[nextPosNum];
            Move();
        }
	}

    private void Move()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextPos, 0.18f);
        if (Vector3.Distance(transform.localPosition, nextPos) <= 0)
        {
            ChangePoint();
        }
    }

    

    public void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void OnBecameInvisible()
    {
        myAnimator.enabled = false;
    }

    private void OnBecameVisible()
    {
        myAnimator.enabled = true;
    }

    void ChangePoint()
    {
        if (nextPos != pathCordinates[pathCordinates.Length - 1])
        {
            nextPosNum++;
            nextPos = pathCordinates[nextPosNum];
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(this);
        }
    }
}
