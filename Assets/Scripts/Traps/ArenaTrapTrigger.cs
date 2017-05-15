using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ArenaTrapTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject leftDoor;
    [SerializeField]
    private GameObject rightDoor;
    [SerializeField]
    private GameObject fightSign;

    private DoorOpen LDoor;
    private DoorOpen RDoor;
    private DoorOpen fight;

    private void Start()
    {
        LDoor = leftDoor.GetComponentInChildren<DoorOpen>();
        RDoor = rightDoor.GetComponentInChildren<DoorOpen>();
        fight = fightSign.GetComponentInChildren<DoorOpen>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LDoor.Activate();
            RDoor.Activate();
            fight.Activate();
        }
    }


}
