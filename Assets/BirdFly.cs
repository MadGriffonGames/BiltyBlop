using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFly : MonoBehaviour {
    [SerializeField]
    GameObject trigger;
    bool isTriggered = false;
    public Transform target;
    Animator myAnimator;
    Vector3 nextPos;
    int nextPosNum = 1;
    [SerializeField]
    GameObject shadow;
    [SerializeField]
    public UnityEngine.Transform[] pathPoints;
    public Vector3[] pathCordinates;

    // Use this for initialization
    void Start () {
        myAnimator = GetComponent<Animator>();
        myAnimator.enabled = false;
        pathCordinates = new Vector3[pathPoints.Length];
        int i = 0;
        
        foreach (var point in pathPoints)
        {
            pathCordinates[i] = pathPoints[i].localPosition;
            /*if (transform.localScale.x <= 0)
            {
                float delta = transform.localPosition.x - pathCordinates[i].x;
                pathCordinates[i].x = transform.localPosition.x + delta;
                Debug.Log(pathCordinates[i].x);
            }*/
            i++;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isTriggered)
        {
            int i = 0;
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
        if (other.transform.CompareTag("Player"))
        {
            if (transform.localScale.x <= 0)
                transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
            transform.localRotation = Quaternion.Euler(0, 0, -5f);
            myAnimator.SetTrigger("isTrigger");
            isTriggered = true;
            shadow.SetActive(false);
        }
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
