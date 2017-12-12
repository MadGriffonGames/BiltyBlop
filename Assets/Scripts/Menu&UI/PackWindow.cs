using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class PackWindow : MonoBehaviour
{
    [SerializeField]
    Button buyButton;
    [SerializeField]
    int productId;
    [SerializeField]
    Text timerText;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject packSign;

    TimeSpan timer; 
    TimeSpan hours48;
    DateTime lastOpenDate;

    private void Awake()
    {
        hours48 = new TimeSpan(48,0,0);
        timer = new TimeSpan();
        lastOpenDate = new DateTime();
    }

    private void Update()
    {
        if (timerText != null)
        {
            if (timer > TimeSpan.Zero)
            {
                ConverDateTimeToText();
            }
        }
    }

    public void BuyPack(int packId)
    {
        PurchaseManager.Instance.BuyNonConsumable(packId);
        fade.SetActive(false);
        packSign.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyPack(productId));
        if (timerText != null)
        {
            if (!PlayerPrefs.HasKey("StarterPackOpenDate"))
            {
                PlayerPrefs.SetString("StarterPackOpenDate", DateTime.Now.ToString());
                lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("StarterPackOpenDate"));
                ConverDateTimeToText();
            }
            else
            {
                lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("StarterPackOpenDate"));
                ConverDateTimeToText();
            }
        }
    }

    void ConverDateTimeToText()
    {
        string tmp;
        timer = hours48 + (lastOpenDate - DateTime.Now);
        tmp = "" + (timer.Hours + timer.Days * 24).ToString() + ":" + timer.Minutes.ToString() + ":" + timer.Seconds.ToString();
        timerText.text = tmp;
    }
}
