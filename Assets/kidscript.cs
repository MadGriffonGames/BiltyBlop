using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class kidscript : MonoBehaviour
{
    UnityArmatureComponent myArmature;

    private void Start()
    {
        myArmature = GetComponent<UnityArmatureComponent>();
        
    }
    private void Update()
    {
        Debug.Log(myArmature.armature);
    }
}
