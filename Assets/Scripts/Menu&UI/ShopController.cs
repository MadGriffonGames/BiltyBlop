using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

    public GameObject[] shops;
    [SerializeField]
    Button leftButton;
    [SerializeField]
    Button middleButton;
    [SerializeField]
    Button rightButton;

    public string[] shopNames;
    private int activeShopNumber;
    private int shopsCount;
    private static int buttonsCount = 3;

	private void Start ()
    {
        shopsCount = shops.Length;
        shopNames = new string[shopsCount];
        for (int i = 0; i < shopsCount; i++)
        {
            shopNames[i] = shops[i].gameObject.name;
        }
        activeShopNumber = 1;

        SetButtonText(rightButton, activeShopNumber + 1);
        SetButtonText(middleButton, activeShopNumber);
        SetButtonText(leftButton, activeShopNumber - 1);
        shops[activeShopNumber].SetActive(true);
    }


    public void ChangeShopNames(int inc)
    {
        shops[activeShopNumber].SetActive(false);
        activeShopNumber = (activeShopNumber + inc) % (shopsCount);
        shops[activeShopNumber].SetActive(true);
        
        if (activeShopNumber == shopsCount - 1)  // n-1 n 1
        {
            SetButtonText(rightButton, 0);
            SetButtonText(middleButton, shopsCount - 1);
            SetButtonText(leftButton, shopsCount - 2);
        }
        else if (activeShopNumber == 0)          // n 1 2
        {
            SetButtonText(rightButton, 1);
            SetButtonText(middleButton, 0);
            SetButtonText(leftButton, shopsCount - 1);
        }
        else
        {
            SetButtonText(rightButton, activeShopNumber + 1);
            SetButtonText(middleButton, activeShopNumber);
            SetButtonText(leftButton, activeShopNumber - 1);
        }

    }

    private void SetButtonText(Button button, int shopNumber)
    {
        button.GetComponentInChildren<Text>().text = shopNames[shopNumber];
    }


}
