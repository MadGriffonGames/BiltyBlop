using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {

    [SerializeField]
    private GameObject door;
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject gear;

    [SerializeField]
    private Sprite leverLeft;

    [SerializeField]
    private Sprite leverRight;
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
            if (direction) gameObject.GetComponent<SpriteRenderer>().sprite = leverRight;
            else gameObject.GetComponent<SpriteRenderer>().sprite = leverLeft;
        }
        
      }

   


    // Update is called once per frame
    void Update () {
        if (isMoved)
        { 
            Quaternion rot = gear.transform.localRotation;
            float new_z = rot.z;
            step = speed * Time.deltaTime;
            if (direction)
            {
                new_z += speed/200;
                gear.transform.localRotation = new Quaternion(rot.x, rot.y, new_z, rot.w);//Quaternion.Euler(0, 0, (speed+prev.z));
                door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, posB, step);
                if (Vector3.Distance(door.transform.localPosition, posB) == 0) isMoved = false;
            }
            else
            {
                new_z -= speed/200;
                gear.transform.localRotation = new Quaternion(rot.x, rot.y, new_z, rot.w);
                door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, startPos, step);
                if (Vector3.Distance(door.transform.localPosition, startPos) == 0) isMoved = false;
            }
        }
    }
}
