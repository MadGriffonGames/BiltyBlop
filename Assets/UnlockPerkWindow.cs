using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPerkWindow : MonoBehaviour {

    // HANDLING ALL WINDOW EVENTS

    [SerializeField]
    GameObject buyCoinsButton;
    [SerializeField]
    GameObject buyCrystalsButton;
    [SerializeField]
    GameObject perkName;
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


    public void SetWindowWithPerkNumber(int perkOrderNumber)
    {
        int perkNumber = 0;
        for (int i = 0; i < PerksSwipeMenu.Instance.perkPrefabs.Length; i++)
        {
            if (PerksSwipeMenu.Instance.perkPrefabs[i].GetComponent<PerkPrefab>().orderNumber == perkOrderNumber)
            {
                perkNumber = i;
                break;
            }
        }
        PerkPrefab perk = PerksSwipeMenu.Instance.perkPrefabs[perkNumber].gameObject.GetComponent<PerkPrefab>();
        perkName.GetComponent<Text>().text = perk.shopName;

        // perkNumber - number of chosen perk in perkPrefabs[]

        if (perk.isLocked)
        {
            if (perk.coinCost == 0 && perk.crystalCost != 0)
            {
                buyCrystalsButton.transform.localPosition = onebuttonTransform.localPosition;
                buyCrystalsButton.GetComponentInChildren<Text>().text = perk.crystalCost.ToString();

                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuyPerkByCrystals(perkNumber));

                buyCoinsButton.gameObject.SetActive(false);
            }
            else if (perk.crystalCost == 0 && perk.coinCost != 0)
            {
                buyCoinsButton.transform.localPosition = onebuttonTransform.localPosition;
                buyCoinsButton.GetComponentInChildren<Text>().text = perk.coinCost.ToString();

                buyCoinsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuyPerkByCoins(perkNumber));

                buyCrystalsButton.gameObject.SetActive(false);
            }
            else if (perk.crystalCost == 0 && perk.coinCost == 0)
            {

            }
            else
            {
                ResetButtons();
                buyCrystalsButton.GetComponentInChildren<Text>().text = perk.crystalCost.ToString();
                buyCoinsButton.GetComponentInChildren<Text>().text = perk.coinCost.ToString();

                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCrystalsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuyPerkByCrystals(perkNumber));
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                buyCoinsButton.gameObject.GetComponent<Button>().onClick.AddListener(() => CanBuyPerkByCoins(perkNumber));
            }
        }
    }

    public void CanBuyPerkByCrystals(int perkNmber)
    {
        if (PerksSwipeMenu.Instance.CanUnlockPerkByCrystals(perkNmber))
        {
            // ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА ПЕРКА
            fade.gameObject.SetActive(true);
            closeErrorWindowButton.gameObject.SetActive(true);
            errorWindow.gameObject.SetActive(true);
            errorWindow.GetComponentInChildren<Text>().text = "PERK UNLOCKED";
        }
        else
        {
            fade.gameObject.SetActive(true);
            closeErrorWindowButton.gameObject.SetActive(true);
            errorWindow.gameObject.SetActive(true);
            errorWindow.GetComponentInChildren<Text>().text = "NOT ENOUGH CRYSTALS";
        }
    }
    public void CanBuyPerkByCoins(int perkNumber)
    {
        if (PerksSwipeMenu.Instance.CanUnlockPerkByCoins(perkNumber))
        {
            // ПРОИСХОДИТ АНИМАЦИЯ РАЗЛОКА ПЕРКА
            fade.gameObject.SetActive(true);
            closeErrorWindowButton.gameObject.SetActive(true);
            errorWindow.gameObject.SetActive(true);
            errorWindow.GetComponentInChildren<Text>().text = "PERK UNLOCKED";
        }
        else
        {
            fade.gameObject.SetActive(true);
            closeErrorWindowButton.gameObject.SetActive(true);
            errorWindow.gameObject.SetActive(true);
            errorWindow.GetComponentInChildren<Text>().text = "NOT ENOUGH COINS";
        }
    }

    public void CloseErrorWindow()
    {
        errorWindow.gameObject.SetActive(false);
        fade.gameObject.SetActive(false);
        closeErrorWindowButton.gameObject.SetActive(false);
    }
    private void ResetButtons()
    {
        buyCrystalsButton.gameObject.SetActive(true);
        buyCoinsButton.gameObject.SetActive(true);
        buyCoinsButton.transform.localPosition = coinButtonTransform.localPosition;
        buyCrystalsButton.transform.localPosition = crystalButtonTransform.transform.localPosition;
    }
}
