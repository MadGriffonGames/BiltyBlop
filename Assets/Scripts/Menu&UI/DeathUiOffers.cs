using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUiOffers : MonoBehaviour
{
    [SerializeField]
    BuyItemWindow buyItemWindow;
    [SerializeField]
    GameObject fade;

    public void OpenBuyWindow(string itemName)
    {
        buyItemWindow.gameObject.SetActive(true);
        fade.SetActive(true);
        buyItemWindow.SetBuyItemWindow(itemName);
    }
}
