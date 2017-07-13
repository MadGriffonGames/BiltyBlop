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
    Dictionary<string, GameObject> skins;
    [SerializeField]
    Text txt;
    [SerializeField]
    GameObject armatureObject;
    [SerializeField]
    GameObject buySkinWindow;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject closeBuyWindowButton;
    [SerializeField]
    GameObject skinBuyTransform;

    private string currentSkinName;
    private int skinCost;
    private GameObject skinPrefab;
    public MeshRenderer[] skinMeshes;

    void Start ()
    {
        currentSkinName = PlayerPrefs.GetString("Skin");

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
            skinPrefab = Instantiate(skins[skinName], gameObject.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
        }
        else
        {
            skinPrefab = Instantiate(skins["Classic"], gameObject.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
        }

        myArmature = GetComponentInChildren<UnityArmatureComponent>();
        myArmature.animation.Play("victory_idle");
    }

    private void Update()
    {
        txt.text = currentSkinName;
    }

    public int SkinCost()  // returns cost of current skin
    {
        return skinCost;
    }

    public void ChangeSkin(string skinName)
    {
        myArmature = GetComponentInChildren<UnityArmatureComponent>();
        Destroy(myArmature.gameObject);
        if (skins.ContainsKey(skinName))
        {
            skinPrefab = Instantiate(skins[skinName], gameObject.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
            currentSkinName = skinName;
            skinCost = int.Parse(skinPrefab.gameObject.GetComponentInChildren<Text>().text);
        }
        else
        {
            skinPrefab = Instantiate(skins["Classic"], gameObject.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
            currentSkinName = "Classic";
            skinCost = int.Parse(skinPrefab.gameObject.GetComponentInChildren<Text>().text);
        }

        if (!SkinManager.Instance.isSkinUnlocked(currentSkinName)) // затемнять скин или что-то такое если он не разблокирован
        {

            //skinMeshes = myArmature.gameObject.GetComponentsInChildren<MeshRenderer>();
            ////Debug.Log(skinMeshes.Length);
            //foreach (MeshRenderer meshPart in skinMeshes)
            //{
                
            //    Material newMat = new Material(Shader.Find("Sprites/Diffuse"));
                
            //    //newMat.SetColor(86, Color.black);
            //    newMat.shader = Shader.Find("Sprites/Diffuse");

            //    meshPart.material.color = new Color(81,61,61);
            //    Debug.Log(meshPart.material);
            //}
            
        }

        myArmature = GetComponentInChildren<UnityArmatureComponent>();
        myArmature.animation.Play("victory_idle");
    }

    public string CurrentSkinName() // returns corrent skin name (not applied for game, only in shop)
    {
        return currentSkinName;
    }

    public void ApplySkin()  // choosing skin
    {
        PlayerPrefs.SetString("Skin", currentSkinName);
    }
    public void UnlockSkin()  // unlocking new skin
    {
        SkinManager.Instance.UnlockSkin(currentSkinName);
    }

    public void OpenBuySkinWindow()
    {
        buySkinWindow.SetActive(true);
        fade.SetActive(true);
        Debug.Log(skinPrefab.gameObject.name);
        closeBuyWindowButton.SetActive(true);
        buySkinWindow.GetComponentInChildren<Text>().text = currentSkinName;
        skinPrefab.transform.SetParent(skinBuyTransform.transform);
        skinPrefab.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void CloseBuySkinWindow()
    {
        skinPrefab.transform.SetParent(transform);
        skinPrefab.transform.localPosition = new Vector3(0, 0, 0);
        fade.SetActive(false);
        closeBuyWindowButton.SetActive(false);
        buySkinWindow.SetActive(false);
    }
}
