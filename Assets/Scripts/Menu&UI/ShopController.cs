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

    public string[] shopNames;
    private int activeShopNumber;
    private int shopsCount;

    private void Update()
    {
        UpdateMoneyValues(); // так, ибо почему-то пообращению к методу он не обновляет значенияж
    }

    public void UpdateMoneyValues()
    {
        coinTxt.text = PlayerPrefs.GetInt("Coins").ToString();
        crystalTxt.text = PlayerPrefs.GetInt("Crystals").ToString();
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
