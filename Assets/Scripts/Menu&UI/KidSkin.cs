using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DragonBones;
using UnityEngine;

public class KidSkin : MonoBehaviour
{
    private static KidSkin instance;
    public static KidSkin Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<KidSkin>();
            return instance;
        }
    }

    UnityArmatureComponent myArmature;
    private GameObject skinPrefab;

    public void ChangeSkin(int skinNumber)
    {
        myArmature = GetComponentInChildren<UnityArmatureComponent>();
        Destroy(myArmature.gameObject);
        skinPrefab = Instantiate(SkinManager.Instance.skinPrefabs[skinNumber], gameObject.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
        skinPrefab.gameObject.transform.localScale = new Vector3(30, 30, 30);
        myArmature = GetComponentInChildren<UnityArmatureComponent>();
        myArmature.animation.Play("victory_idle");
    }

}
