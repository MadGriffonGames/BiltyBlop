using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class testtyp : MonoBehaviour
{
    UnityArmatureComponent arm;
    List<Slot> slots;

	// Use this for initialization
	void Start () {
        arm = GetComponent<UnityArmatureComponent>();
        slots = arm.armature.GetSlots();
        foreach (Slot slot in slots)
        {
            slot.displayIndex = 0;
            slot.displayController = "none";
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Slot slot in slots)
            {
                slot.displayIndex += 1;
            }
        }
	}
}
