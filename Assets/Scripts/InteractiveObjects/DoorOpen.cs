using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

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
    bool isBlocked = false; // true: block lever after single use
    bool direction = false; // true is forward, false is backward

    void Start()
    {
        startPos = door.transform.localPosition;
        posB = transformPosB.localPosition;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            
            if (!isBlocked)
            {
                isMoved = true;
                direction = !direction;
                isBlocked = true;
                if (direction) gameObject.GetComponent<SpriteRenderer>().sprite = leverRight;
                else gameObject.GetComponent<SpriteRenderer>().sprite = leverLeft;
            }
        }

    }
    public void Activate()
    {
        direction = !direction;
        isMoved = true;
    }
    void Update()
    {
        
            if (isMoved)
            {
                Quaternion rot = gear.transform.localRotation;
                float new_z = rot.z;
                step = speed * Time.deltaTime;
                if (direction)
                {
                    new_z += speed / 200;
                    gear.transform.localRotation = new Quaternion(rot.x, rot.y, new_z, rot.w);
                    door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, posB, step * 2);
                    if (Vector3.Distance(door.transform.localPosition, posB) == 0)
                    {
                        isMoved = false;
                    }
                }
                else
                {
                    new_z += speed / 200;
                    gear.transform.localRotation = new Quaternion(rot.x, rot.y, new_z, rot.w);
                    door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, startPos, step * 2);
                    if (Vector3.Distance(door.transform.localPosition, startPos) == 0)
                    {
                        isMoved = false;
                    }
                }
            }
        
    }
}
