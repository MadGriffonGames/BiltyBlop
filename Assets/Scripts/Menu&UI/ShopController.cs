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
    [SerializeField]
    Button skinsButton;
    [SerializeField]
    Button itemsButton;
    [SerializeField]
    Button perksButton;
	[SerializeField]
	Button gemsButton;
	[SerializeField]
	Button swordsButton;

    [SerializeField]
    Text coinTxt;
    [SerializeField]
    Text crystalTxt;

    public string[] shopNames;
    private int activeShopNumber;
    private int shopsCount;


    private void Awake ()
    {
        perksButton.GetComponentInChildren<Text>().text = "PERKS";
        itemsButton.GetComponentInChildren<Text>().text = "ITEMS";
        skinsButton.GetComponentInChildren<Text>().text = "SKINS";
		gemsButton.GetComponentInChildren<Text>().text = "GEMS";
		swordsButton.GetComponentInChildren<Text>().text = "SWORDS";
    }

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
            if (number == i)
            {
                shops[i].SetActive(true);
            }
            else
                shops[i].SetActive(false);
        }
    }

    private void SetButtonText(Button button, int shopNumber)
    {
        button.GetComponentInChildren<Text>().text = shopNames[shopNumber];
    }


}
