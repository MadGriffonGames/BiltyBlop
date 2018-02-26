using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuyCheckpointsUI : MonoBehaviour, IAdsPlacement
{
    const int FREE_CHECKPOINTS_GIFT = 1;
    const int CRYSTAL_PRICE = 4;
    const int NOT_PREMIUM_ATTEMPS = 3;

    [SerializeField]
    GameObject videoButton;
    [SerializeField]
    GameObject coinsButton;
    [SerializeField]
    Text coinsPrice;
    [SerializeField]
    GameObject crystalsButton;
    [SerializeField]
    GameObject freeButton;

    int coinsPriceInt;

    int notPremiumAttemps = NOT_PREMIUM_ATTEMPS;

    private void OnEnable()
    {
        if (notPremiumAttemps > 0)
        {
            if (PlayerPrefs.GetInt("NoAds") != 0)
            {
                videoButton.SetActive(false);
                coinsButton.SetActive(false);
                crystalsButton.SetActive(false);

                freeButton.SetActive(true);
            }
            else
            {
                videoButton.SetActive(true);
                coinsButton.SetActive(true);
                switch (notPremiumAttemps)
                {
                    case 3:
                        coinsPrice.text = "100";
                        break;
                    case 2:
                        coinsPrice.text = "200";
                        break;
                    case 1:
                        coinsPrice.text = "300";
                        break;
                }
                coinsPriceInt = int.Parse(coinsPrice.text);
                crystalsButton.SetActive(false);
                freeButton.SetActive(false);
            }
        }
        else
        {
            videoButton.SetActive(false);
            coinsButton.SetActive(false);
            freeButton.SetActive(false);

            crystalsButton.SetActive(true);
        }
    }

    public void VideoButton()
    {
        MetricaManager.Instance.rewardedCheckpoints++;

        AdsManager.Instance.ShowRewardedVideo(this);

        AppMetrica.Instance.ReportEvent("#CHECKPOINTS_USE Checkpoints bought for Rewarded Video in " + MetricaManager.Instance.currentLevel);
        DevToDev.Analytics.CustomEvent("#CHECKPOINTS_USE Checkpoints bought for Rewarded Video in " + MetricaManager.Instance.currentLevel);
    }

    public void CoinsButton()
    {
        if (PlayerPrefs.GetInt("Coins") >= coinsPriceInt)
        {
            MetricaManager.Instance.coinCheckpoints++;

            Player.Instance.freeCheckpoints = FREE_CHECKPOINTS_GIFT;
            DeathUI.Instance.UpdateFreeCheckpointsCounter();

            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - coinsPriceInt);
            GameManager.collectedCoins -= coinsPriceInt;

            notPremiumAttemps--;

            AppMetrica.Instance.ReportEvent("#CHECKPOINTS_USE Checkpoints bought for Coins in " + MetricaManager.Instance.currentLevel);
            DevToDev.Analytics.CustomEvent("#CHECKPOINTS_USE Checkpoints bought for Coins in " + MetricaManager.Instance.currentLevel);
            this.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Coins") + GameManager.lvlCollectedCoins >= coinsPriceInt)
        {
            MetricaManager.Instance.coinCheckpoints++;

            Player.Instance.freeCheckpoints = FREE_CHECKPOINTS_GIFT;
            DeathUI.Instance.UpdateFreeCheckpointsCounter();

            GameManager.lvlCollectedCoins -= coinsPriceInt - PlayerPrefs.GetInt("Coins");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - PlayerPrefs.GetInt("Coins"));
            GameManager.collectedCoins -= coinsPriceInt;

            notPremiumAttemps--;

            AppMetrica.Instance.ReportEvent("#CHECKPOINTS_USE Checkpoints bought for Coins in " + MetricaManager.Instance.currentLevel);
            DevToDev.Analytics.CustomEvent("#CHECKPOINTS_USE Checkpoints bought for Coins in " + MetricaManager.Instance.currentLevel);
            this.gameObject.SetActive(false);
        }
        
    }

    public void CrystalsButton()
    {
        if (PlayerPrefs.GetInt("Crystals") >= CRYSTAL_PRICE)
        {
            MetricaManager.Instance.crystalCheckpoints++;

            Player.Instance.freeCheckpoints = FREE_CHECKPOINTS_GIFT;
            DeathUI.Instance.UpdateFreeCheckpointsCounter();

            PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") - CRYSTAL_PRICE);
            GameManager.crystalTxt.text = PlayerPrefs.GetInt("Crystals").ToString();

            notPremiumAttemps--;

            AppMetrica.Instance.ReportEvent("#CHECKPOINTS_USE Checkpoints bought for Crystals in " + MetricaManager.Instance.currentLevel);
            DevToDev.Analytics.CustomEvent("#CHECKPOINTS_USE Checkpoints bought for Crystals in " + MetricaManager.Instance.currentLevel);

            this.gameObject.SetActive(false);
        }
        else
        {
            PurchaseManager.Instance.BuyConsumable(1);
        }
    }

    public void FreeButton()
    {
        Player.Instance.freeCheckpoints = FREE_CHECKPOINTS_GIFT;
        DeathUI.Instance.UpdateFreeCheckpointsCounter();
        notPremiumAttemps--;
        this.gameObject.SetActive(false);
    }

    public void Skip()
    {
        this.gameObject.SetActive(false);
    }

    public void OnRewardedVideoWatched()
    {
        this.gameObject.SetActive(false);

        AppMetrica.Instance.ReportEvent("#CHECKPOINT_VIDEO watched");
        DevToDev.Analytics.CustomEvent("#CHECKPOINT_VIDEO watched");

        Player.Instance.freeCheckpoints = FREE_CHECKPOINTS_GIFT;
        DeathUI.Instance.UpdateFreeCheckpointsCounter();

        notPremiumAttemps--;
    }

    public void OnRewardedVideoFailed()
    {

    }
}
