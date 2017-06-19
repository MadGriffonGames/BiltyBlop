using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFly : MonoBehaviour {
    [SerializeField]
    GameObject trigger;
    bool isTriggered = false;
    public Transform target;
    Animator myAnimator;
    [SerializeField]
    GameObject shadow;

	// Use this for initialization
	void Start () {
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isTriggered)
        {
            Move();
        }
	}

    private void Move()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target.localPosition, 0.2f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            myAnimator.SetTrigger("isTrigger");
            isTriggered = true;
            shadow.SetActive(false);
        }
    }
}
