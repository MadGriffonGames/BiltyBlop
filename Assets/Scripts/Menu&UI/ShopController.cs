using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

    private static ShopController instance;
    public static ShopController Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<ShopController>();
            return instance;
        }
    }

    public GameObject[] shops;
	public Button[] buttons;

    [SerializeField]
    Text coinTxt;
    [SerializeField]
    Text crystalTxt;
	[SerializeField]
	Text HP;
	[SerializeField]
	Text DMG;
	[SerializeField]
	Text IMM;
	[SerializeField]
	Text SPD;
	[SerializeField]
	Text TM;
	[SerializeField]
	Text THR;

    private int activeShopNumber;
    private int shopsCount;

    private void Update()
    {
        UpdateMoneyValues();
		UpdateItemsValues ();// так, ибо почему-то пообращению к методу он не обновляет значенияж
    }

    public void UpdateMoneyValues()
    {
        coinTxt.text = PlayerPrefs.GetInt("Coins").ToString();
        crystalTxt.text = PlayerPrefs.GetInt("Crystals").ToString();
    }

	public void UpdateItemsValues()
	{
		HP.text = Inventory.Instance.GetItemCount ("HealthPot").ToString();
		DMG.text = Inventory.Instance.GetItemCount ("DamageBonus").ToString();
		IMM.text = Inventory.Instance.GetItemCount ("ImmortalBonus").ToString();
		SPD.text = Inventory.Instance.GetItemCount ("SpeedBonus").ToString();
		TM.text = Inventory.Instance.GetItemCount ("TimeBonus").ToString();
		THR.text = Inventory.Instance.GetItemCount ("ClipsCount").ToString();
	}

    public void ActivateShop(int number)
    {
        for (int i = 0; i < shops.Length; i++)
        {
			if (number == i) {
				shops [i].SetActive (true);
				ActivateButton (buttons [i]);				
			}  
			else 
			{
				shops [i].SetActive (false);
				DisactivateButton (buttons [i]);
			}	
        }
    }

    public void ActivateButton(Button button)
    {
		button.GetComponent<Image> ().color = ToColor ("FFFFFFF3", 255);
    }
	public void DisactivateButton(Button button)
	{
		button.GetComponent<Image> ().color = ToColor ("C3C3C3DB", 219);
	}

	public Color32 ToColor(string hexString, byte A)
	{
		int hexVal = int.Parse (hexString, System.Globalization.NumberStyles.HexNumber);
		byte R = (byte)((hexVal >> 16) & 0xFF);
		byte G = (byte)((hexVal >> 8) & 0xFF);
		byte B = (byte)((hexVal) & 0xFF);
		return new Color32 (R,G,B,A);
	}
}
