using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class KidSkin : MonoBehaviour
{
    UnityArmatureComponent myArmature;

    void Start ()
    {
        myArmature = GetComponent<UnityArmatureComponent>();
        myArmature.Dispose(false);//destroy all child game objects
        UnityFactory.factory.BuildArmatureComponent(PlayerPrefs.GetString("Skin", "classickidarian") , null, null, null, gameObject);
        myArmature.animation.Play("victory_idle");
    }

    public void ChangeSkin(string skinName)
    {
        myArmature.Dispose(false);//destroy all child game objects
        UnityFactory.factory.BuildArmatureComponent(skinName, null, null, null, gameObject);
        PlayerPrefs.SetString("Skin", skinName);
        myArmature.animation.Play("victory_idle");
    }

}
