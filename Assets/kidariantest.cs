using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class kidariantest : MonoBehaviour
{
    UnityArmatureComponent myArmature;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            myArmature = GetComponent<UnityArmatureComponent>();
            myArmature.Dispose(false);
            UnityFactory.factory.BuildArmatureComponent("blackkidarian", null, null, null, gameObject);
            myArmature.animation.Play("idle");
        }
    }
}
