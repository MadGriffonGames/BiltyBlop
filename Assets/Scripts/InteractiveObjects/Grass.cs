using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class Grass : MonoBehaviour
{
    protected UnityArmatureComponent armature;

    void Start ()
    {
        armature = GetComponent<UnityArmatureComponent>();
        armature.enabled = false;
    }

    private void OnBecameVisible()
    {
        armature.enabled = true;
    }

    private void OnBecameInvisible()
    {
        armature.enabled = false;
    }
}
