using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour {
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private float speedUp;
    [SerializeField]
    private float speedDown;

    [SerializeField]
    private Transform transformPosA;

    [SerializeField]
    private Transform transformPosB;

    [SerializeField]
    private Collider2D doorCollider;
    [SerializeField]
    private Collider2D attackTriger;

    private Vector3 posA;
    private Vector3 posB;

    float step;
    bool direction = true; // true is forward, false is backward

    // Use this for initialization
    void Start()
    {
        posA = door.transform.localPosition;
        posB = transformPosB.localPosition;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
            if(!direction)
        {
            StartCoroutine(Player.Instance.TakeDamage());
            Physics2D.IgnoreCollision(doorCollider, other, true);
        }
            
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            if (direction)
            {
                Physics2D.IgnoreCollision(doorCollider, other, false);
            }
    }

    // Update is called once per frame
    void Update ()
    {
        
        if (direction)
        {
            step = speedUp * Time.deltaTime;
            door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, posB, step);
            if (Vector3.Distance(door.transform.localPosition, posB) == 0) direction = false;
        }
        else
        {
            step = speedDown * Time.deltaTime;
            door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, posA, step);
            if (Vector3.Distance(door.transform.localPosition, posA) == 0) direction = true;
        }
    }
}
