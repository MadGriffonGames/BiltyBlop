using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LowItemBar : MonoBehaviour
{
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

    void Update ()
    {
        UpdateMoneyValues();
        UpdateItemsValues();
    }

    public void UpdateMoneyValues()
    {
        coinTxt.text = PlayerPrefs.GetInt("Coins").ToString();
        crystalTxt.text = PlayerPrefs.GetInt("Crystals").ToString();
    }

    public void UpdateItemsValues()
    {
        HP.text = Inventory.Instance.GetItemCount("HealthPot").ToString();
        DMG.text = Inventory.Instance.GetItemCount("DamageBonus").ToString();
        IMM.text = Inventory.Instance.GetItemCount("ImmortalBonus").ToString();
        SPD.text = Inventory.Instance.GetItemCount("SpeedBonus").ToString();
        TM.text = Inventory.Instance.GetItemCount("TimeBonus").ToString();
        THR.text = Inventory.Instance.GetItemCount("ClipsCount").ToString();
    }
}
