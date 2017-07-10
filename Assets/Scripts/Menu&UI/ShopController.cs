using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

    public GameObject[] shops;
    [SerializeField]
    Button skinsButton;
    [SerializeField]
    Button itemsButton;
    [SerializeField]
    Button perksButton;

    public string[] shopNames;
    private int activeShopNumber;
    private int shopsCount;


    private void Start ()
    {
        perksButton.GetComponentInChildren<Text>().text = "PERKS";
        itemsButton.GetComponentInChildren<Text>().text = "ITEMS";
        skinsButton.GetComponentInChildren<Text>().text = "SKINS";
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
