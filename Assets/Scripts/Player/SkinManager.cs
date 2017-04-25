using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer LeftHandUpper;
    [SerializeField]
    SpriteRenderer LeftHandMiddle;
    [SerializeField]
    SpriteRenderer HandWithSword;
    [SerializeField]
    SpriteRenderer RightHandUpper;
    [SerializeField]
    SpriteRenderer RightHandMiddle;
    [SerializeField]
    SpriteRenderer Hand;
    [SerializeField]
    SpriteRenderer Face;
    [SerializeField]
    SpriteRenderer Hemlet;
    [SerializeField]
    SpriteRenderer Body;
    [SerializeField]
    SpriteRenderer LeftUpperLeg;
    [SerializeField]
    SpriteRenderer LeftLeg;
    [SerializeField]
    SpriteRenderer LeftShoe;
    [SerializeField]
    SpriteRenderer RightUpperLeg;
    [SerializeField]
    SpriteRenderer RightLeg;
    [SerializeField]
    SpriteRenderer RightShoe;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Skin"))
        {
            PlayerPrefs.SetString("Skin", "Kid");
        }
        RightShoe.sprite = Resources.Load<Sprite>("Sprites/Skins/" + PlayerPrefs.GetString("Skin") + "/Shoe") as Sprite;
    }
}
