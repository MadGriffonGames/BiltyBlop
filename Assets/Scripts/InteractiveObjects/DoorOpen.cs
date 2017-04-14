using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {

	[SerializeField]
       private GameObject door;
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform transformPosB;

    private Vector3 startPos;
    private Vector3 posB;

    float step;
    bool isMoved = false;
    bool direction = false; // true is forward, false is backward

    // Use this for initialization
    void Start () {

        startPos = door.transform.localPosition;
        posB = transformPosB.localPosition;
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Sword"))
        {
            isMoved = true;
            direction = !direction;
        }
        
      }

   


    // Update is called once per frame
    void Update () {
        if (isMoved)
        {
            step = speed * Time.deltaTime;
            if (direction)
            {
                door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, posB, step);
                if (Vector3.Distance(door.transform.localPosition, posB) == 0) isMoved = false;
            }
            else
            {
                door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, startPos, step);
                if (Vector3.Distance(door.transform.localPosition, startPos) == 0) isMoved = false;
            }
        }
    }
}
