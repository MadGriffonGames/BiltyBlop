using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class kidariantest : MonoBehaviour
{
    UnityArmatureComponent myArmature;
    private void Start()
    {
        Slot slot = myArmature.armature.GetSlot("part");
        if (slot == null)
        {
            Debug.Log(1);
        }
        
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            myArmature = GetComponent<UnityArmatureComponent>();
           
            myArmature.Dispose(false);
            UnityFactory.factory.BuildArmatureComponent("King", null, null, null, gameObject);
            myArmature.animation.Play("idle");
        }
    }
}
