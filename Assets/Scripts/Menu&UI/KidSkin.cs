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
        myArmature._onClear();
        UnityFactory.factory.BuildArmatureComponent(PlayerPrefs.GetString("Skin", "Classic") , null, null, null, gameObject);
        myArmature.sortingLayerName = myArmature.sortingLayerName;
        myArmature.sortingOrder = myArmature.sortingOrder;
        myArmature.animation.Play("victory_idle");
    }

    public void ChangeSkin(string skinName)
    {
        myArmature._onClear();
        UnityFactory.factory.BuildArmatureComponent(skinName, null, null, null, gameObject);
        myArmature.sortingLayerName = myArmature.sortingLayerName;
        myArmature.sortingOrder = myArmature.sortingOrder;
        PlayerPrefs.SetString("Skin", skinName);
        myArmature.animation.Play("victory_idle");
    }

}
