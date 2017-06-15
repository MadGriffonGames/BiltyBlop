using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DragonBones;
using UnityEngine;

public class KidSkin : MonoBehaviour
{
    UnityArmatureComponent myArmature;
    Dictionary<string, GameObject> skins;
    [SerializeField]
    Text txt;
    [SerializeField]
    GameObject armatureObject;

    void Start ()
    {
        //add skin prefabs to dictionary, key in dictionary is name of skin(that equals name of gameObject)
        skins = new Dictionary<string, GameObject>();
        foreach (GameObject skin in SkinManager.Instance.skinPrefabs)
        {
            skins[skin.name] = skin;
        }

        myArmature = GetComponentInChildren<UnityArmatureComponent>();
        Destroy(myArmature.gameObject);

        string skinName = PlayerPrefs.GetString("Skin", "Classic");
        if (skins.ContainsKey(skinName))
        {
            GameObject skinPrefab = Instantiate(skins[skinName], gameObject.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
        }
        else
        {
            GameObject skinPrefab = Instantiate(skins["Classic"], gameObject.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
        }

        myArmature = GetComponentInChildren<UnityArmatureComponent>();
        myArmature.animation.Play("victory_idle");
    }

    private void Update()
    {
        txt.text = PlayerPrefs.GetString("Skin");
    }

    public void ChangeSkin(string skinName)
    {
        myArmature = GetComponentInChildren<UnityArmatureComponent>();
        Destroy(myArmature.gameObject);
        if (skins.ContainsKey(skinName))
        {
            GameObject skinPrefab = Instantiate(skins[skinName], gameObject.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
            PlayerPrefs.SetString("Skin", skinName);
        }
        else
        {
            GameObject skinPrefab = Instantiate(skins["Classic"], gameObject.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
            PlayerPrefs.SetString("Skin", "Classic");
        }
        myArmature = GetComponentInChildren<UnityArmatureComponent>();
        myArmature.animation.Play("victory_idle");
    }

}
