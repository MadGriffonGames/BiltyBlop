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
	private Slot[] skinSlots;
	private Slot swordSlot;
	private int skinIndex;

	public void Awake()
	{
		skinSlots = new Slot[9];
	}	

    public void ChangeSkin(int skinNumber)
    {
        myArmature = GetComponentInChildren<UnityArmatureComponent>();
		skinIndex = SkinManager.Instance.skinPrefabs [skinNumber].GetComponentInChildren<SkinPrefab> ().displayIndex;

		SetSlots ();

		SetIndexes ();

		//myArmature.animation.Play("victory_idle");
    }

	public void SwordInShop()
	{
		swordSlot = myArmature.armature.GetSlot("Sword");
		swordSlot.displayIndex = 0;
	}
	public void AddSlot(string slotName, ref int i)
	{
		skinSlots [i] = myArmature.armature.GetSlot (slotName);
		i++;
	}

	public void SetSlots()
	{
		int i = 0;

		myArmature.zSpace = 0.02f;

		AddSlot("r_hand_2", ref i);
		AddSlot("l_hand_2", ref i);
		AddSlot("leg", ref i);
		AddSlot("leg1", ref i);
		AddSlot("Shoulder_l", ref i);
		AddSlot("Shoulder_r", ref i);
		AddSlot("torso", ref i);
		AddSlot("mex", ref i);
		AddSlot("head", ref i);
	}

	public void SetIndexes()
	{

		for (int j = 0; j < skinSlots.Length; j++)
		{
			skinSlots[j].displayIndex = skinIndex;
		}
	}

}
