﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSkinWindow : MonoBehaviour {


    // HANDLING ALL WINDOW EVENTS

    [SerializeField]
    GameObject buyCoinsButton;
    [SerializeField]
    GameObject buyCrystalsButton;
    [SerializeField]
    GameObject skinName;
    [SerializeField]
    GameObject statsPanel;
    [SerializeField]
    GameObject applyButton;
    [SerializeField]
    GameObject errorWindow;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject closeErrorWindowButton;

    [SerializeField]
    Transform onebuttonTransform;
    [SerializeField]
    Transform crystalButtonTransform;
    [SerializeField]
    Transform coinButtonTransform;

    public void SetWindowWithSkinNumber(int skinNumber)
    {
        
        SkinPrefab skin = SkinManager.Instance.skinPrefabs[skinNumber].gameObject.GetComponent<SkinPrefab>();
        KidSkin.Instance.ChangeSkin(skinNumber);
        skinName.GetComponent<Text>().text = skin.shopName;
        statsPanel.GetComponentInChildren<SkinStatsPanel>().SetAttackIndicators(skin.attackStat);
        statsPanel.GetComponentInChildren<SkinStatsPanel>().SetDefendIndicators(skin.armorStat);
        if (skin.isLocked)
        {
            if (skin.coinCost == 0 && skin.crystalCost != 0)
            {
                buyCrystalsButton.transform.localPosition = onebuttonTransform.localPosition;
                buyCrystalsButton.GetComponentInChildren<Text>().text = skin.crystalCost.ToString();

                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuySkinByCrystals(skinNumber));

                buyCoinsButton.gameObject.SetActive(false);
            }
            else if (skin.crystalCost == 0 && skin.coinCost != 0)
            {
                buyCoinsButton.transform.localPosition = onebuttonTransform.localPosition;
                buyCoinsButton.GetComponentInChildren<Text>().text = skin.coinCost.ToString();
                
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuySkinByCoins(skinNumber));

                buyCrystalsButton.gameObject.SetActive(false);               
            }
            else if (skin.crystalCost == 0 && skin.coinCost == 0)
            {

            }
            else
            {
                ResetButtons();                
                buyCrystalsButton.GetComponentInChildren<Text>().text = skin.crystalCost.ToString();
                buyCoinsButton.GetComponentInChildren<Text>().text = skin.coinCost.ToString();

                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuySkinByCrystals(skinNumber));
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuySkinByCoins(skinNumber));
            }
        }
    }

    public void ApplySkin()
    {
        
    }

    public void CanBuySkinByCrystals (int skinNumber)
    {
        if (SkinManager.Instance.BuySkinByCrystals(skinNumber))
        {
            // ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА СКИНА
        }
        else
        {
            fade.gameObject.SetActive(true);
            closeErrorWindowButton.gameObject.SetActive(true);
            errorWindow.gameObject.SetActive(true);
            errorWindow.GetComponentInChildren<Text>().text = "NOT ENOUGH CRYSTALS";
        }
    }
    public void CanBuySkinByCoins(int skinNumber)
    {
        if (SkinManager.Instance.BuySkinByCoins(skinNumber))
        {
            // ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА СКИНА
        }
        else
        {
            fade.gameObject.SetActive(true);
            closeErrorWindowButton.gameObject.SetActive(true);
            errorWindow.gameObject.SetActive(true);
            errorWindow.GetComponentInChildren<Text>().text = "NOT ENOUGH CRYSTALS";
        }
    }

    public void CloseErrorWindow()
    {
        fade.gameObject.SetActive(false);
        closeErrorWindowButton.gameObject.SetActive(false);
        errorWindow.gameObject.SetActive(false);
    }
    private void ResetButtons()
    {
        buyCrystalsButton.gameObject.SetActive(true);
        buyCoinsButton.gameObject.SetActive(true);
        buyCoinsButton.transform.localPosition = coinButtonTransform.localPosition;
        buyCrystalsButton.transform.localPosition = crystalButtonTransform.transform.localPosition;
    }
}