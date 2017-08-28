using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class kidariantest : MonoBehaviour
{
    UnityArmatureComponent myArmature;

    Slot slot;

    int i = 0;

    private void Start()
    {
        myArmature = GetComponent<UnityArmatureComponent>();
        slot = myArmature.armature.GetSlot("head");
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            i++;
            slot.displayIndex = i;
            if (i == 3)
            {
                i = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            myArmature.armature.animation.FadeIn("victory_idle",-1,-1,0,null,AnimationFadeOutMode.SameLayerAndGroup,false,false);
            //slot.displayIndex = i;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {

            myArmature.armature.animation.Play("attack");
            slot.displayIndex = i;
        }
    }
}
