﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUiOffers : MonoBehaviour
{
    [SerializeField]
    BuyItemWindow buyItemWindow;
    [SerializeField]
    GameObject lowBar;
    [SerializeField]
    GameObject fade;

    public void OpenBuyWindow(string itemName)
    {
        buyItemWindow.gameObject.SetActive(true);
        fade.SetActive(true);
        lowBar.SetActive(true);
        buyItemWindow.SetBuyItemWindow(itemName);
    }
}
