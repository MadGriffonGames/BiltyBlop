using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuyCheckpointsUI : MonoBehaviour
{
    const int FREE_CHECKPOINTS_GIFT = 3;

    [SerializeField]
    GameObject videoButton;
    [SerializeField]
    GameObject coinsButton;
    [SerializeField]
    Text coinsPrice;
    [SerializeField]
    GameObject crystalsButton;
    [SerializeField]
    Text crystalPrice;

    int notPremiumAttemps = 3;

    private void OnEnable()
    {
        if (notPremiumAttemps > 0)
        {
            videoButton.SetActive(true);
            coinsButton.SetActive(true);
            switch (notPremiumAttemps)
            {
                case 3:
                    coinsPrice.text = "25";
                    break;
                case 2:
                    coinsPrice.text = "50";
                    break;
                case 1:
                    coinsPrice.text = "75";
                    break;
            }
            crystalsButton.SetActive(false);
        }
        else
        {
            videoButton.SetActive(false);
            coinsButton.SetActive(false);
            crystalsButton.SetActive(true);
        }
    }

    private void Update()
    {
        if (AdsManager.Instance.isRewardVideoWatched)
        {
            AdsManager.Instance.isRewardVideoWatched = false;

            Player.Instance.freeCheckpoints = FREE_CHECKPOINTS_GIFT;
            DeathUI.Instance.UpdateFreeCheckpointsCounter();

            notPremiumAttemps--;

            this.gameObject.SetActive(false);
        }
    }

    public void VideoButton()
    {
#if UNITY_EDITOR
        AdsManager.Instance.isRewardVideoWatched = true;

#elif UNITY_ANDROID
        AdsManager.Instance.ShowRewardedVideo();//then check if ad was showed in update()

#elif UNITY_IOS
        AdsManager.Instance.ShowRewardedVideo();//then check if ad was showed in update()

#endif
    }

    public void CoinsButton()
    {
        Player.Instance.freeCheckpoints = FREE_CHECKPOINTS_GIFT;
        DeathUI.Instance.UpdateFreeCheckpointsCounter();

        notPremiumAttemps--;

        this.gameObject.SetActive(false);
    }

    public void CrystalsButton()
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
}
