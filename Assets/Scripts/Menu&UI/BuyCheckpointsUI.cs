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
    Text coinsText;
    [SerializeField]
    GameObject crystalsButton;

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
                    coinsText.text = "25";
                    break;
                case 2:
                    coinsText.text = "50";
                    break;
                case 1:
                    coinsText.text = "75";
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

    public void VideoButton()
    {
        Player.Instance.freeCheckpoints = FREE_CHECKPOINTS_GIFT;
        DeathUI.Instance.UpdateFreeCheckpointsCounter();
        notPremiumAttemps--;
        this.gameObject.SetActive(false);
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
