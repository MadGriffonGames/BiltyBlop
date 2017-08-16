using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class kidariantest : MonoBehaviour
{
    UnityArmatureComponent myArmature;

    Slot sl;

    private void Start()
    {
        myArmature.armature.GetSlotByDisplay(gameObject);
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
