using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSkinWindow : MonoBehaviour {


    [SerializeField]
    GameObject buyCoinsButton;
    [SerializeField]
    GameObject buyCrystalsButton;
    [SerializeField]
    GameObject skinName;
    [SerializeField]
    GameObject statsPanel;
    [SerializeField]
    Transform onbuttonTransform;
    [SerializeField]
    Transform crystalButtonTransform;
    [SerializeField]
    Transform coinButtonTransform;

    private void Start()
    {

    }

    public void SetWindowWithSkinNumber(int skinNumber)
    {
        SkinPrefab skin = SkinManager.Instance.skinPrefabs[skinNumber].gameObject.GetComponent<SkinPrefab>();
        skinName.GetComponent<Text>().text = skin.shopName;
        statsPanel.GetComponentInChildren<SkinStatsPanel>().SetAttackIndicators(skin.attackStat);
        statsPanel.GetComponentInChildren<SkinStatsPanel>().SetDefendIndicators(skin.armorStat);
        if (skin.isLocked)
        {
            if (skin.coinCost == 0 && skin.crystalCost != 0)
            {
                buyCrystalsButton.transform.localPosition = onbuttonTransform.localPosition;
                buyCrystalsButton.GetComponentInChildren<Text>().text = skin.crystalCost.ToString();
                buyCoinsButton.gameObject.SetActive(false);
            }
            else if (skin.crystalCost == 0 && skin.coinCost != 0)
            {
                buyCoinsButton.transform.localPosition = onbuttonTransform.localPosition;
                buyCrystalsButton.gameObject.SetActive(false);
                buyCoinsButton.GetComponentInChildren<Text>().text = skin.coinCost.ToString();
            }
            else
            {
                ResetButtons();                
                buyCrystalsButton.GetComponentInChildren<Text>().text = skin.crystalCost.ToString();
                buyCoinsButton.GetComponentInChildren<Text>().text = skin.coinCost.ToString();
            }
        }
    }

    private void ResetButtons()
    {
        buyCrystalsButton.gameObject.SetActive(true);
        buyCoinsButton.gameObject.SetActive(true);
        buyCoinsButton.transform.localPosition = coinButtonTransform.localPosition;
        buyCrystalsButton.transform.localPosition = crystalButtonTransform.transform.localPosition;
    }
}
