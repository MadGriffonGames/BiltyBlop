using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DailyRewardSlot : MonoBehaviour
{
    public int dayNum;

    [SerializeField]
    DailyReward dailyRewardObject;

    [SerializeField]
    Sprite collectedSprite;
    [SerializeField]
    Sprite coinSprite;
    [SerializeField]
    Sprite crystalSprite;
    [SerializeField]
    Sprite hpSprite;
    [SerializeField]
    Sprite ammoSprite;
    [SerializeField]
    Sprite potionsSprite;
    [SerializeField]
    Sprite skinSprite;
    [SerializeField]
    Sprite misteriousSprite;  

    [SerializeField]
    Sprite defaultBack;
    [SerializeField]
    Sprite todayBack;
    [SerializeField]
    Sprite specialBack;
    [SerializeField]
    Sprite skinBack;

    Image slotBackground;
    [SerializeField]
    Image rewardImage;
    [SerializeField]
    Text dayText;
    [SerializeField]
    Text rewardValue;
    [SerializeField]
    Image generalRewardImage;
    [SerializeField]
    Text generalRewardValue;

    private void Awake()
    {
        slotBackground = GetComponent<Image>();
    }

    public void SetReward()
    {
        dayText.text = "Day" + dayNum.ToString();

        if (dayNum == PlayerPrefs.GetInt("RewardDay"))
        {
            slotBackground.sprite = todayBack;
            switch (dayNum)
            {
                case 1:
                    rewardImage.sprite = coinSprite;
                    generalRewardImage.sprite = coinSprite;
                    rewardValue.text = "100";
                    generalRewardValue.text = "100";
                    rewardValue.enabled = true;
                    dailyRewardObject.myReward.SetReward("Coins", 100);
                    GameManager.AddCoins(100);
                    break;
                case 2:
                    rewardImage.sprite = coinSprite;
                    generalRewardImage.sprite = coinSprite;
                    rewardValue.text = "150";
                    generalRewardValue.text = "150";
                    rewardValue.enabled = true;
                    dailyRewardObject.myReward.SetReward("Coins", 150);
                    GameManager.AddCoins(150);
                    break;
                case 3:
                    slotBackground.sprite = todayBack;
                    rewardImage.sprite = misteriousSprite;
                    generalRewardImage.sprite = hpSprite;
                    generalRewardValue.text = "1";
                    rewardValue.enabled = false;
                    dailyRewardObject.myReward.SetReward("Item", 1, Inventory.HEAL);
                    Inventory.Instance.AddItem(Inventory.HEAL, 1);
                    break;
                case 4:
                    rewardImage.sprite = coinSprite;
                    generalRewardImage.sprite = coinSprite;
                    rewardValue.text = "200";
                    generalRewardValue.text = "200";
                    rewardValue.enabled = true;
                    dailyRewardObject.myReward.SetReward("Coins", 200);
                    GameManager.AddCoins(200);
                    break;
                case 5:
                    rewardImage.sprite = coinSprite;
                    generalRewardImage.sprite = coinSprite;
                    rewardValue.text = "250";
                    generalRewardValue.text = "250";
                    rewardValue.enabled = true;
                    dailyRewardObject.myReward.SetReward("Coins", 250);
                    GameManager.AddCoins(250);
                    break;
                case 6:
                    slotBackground.sprite = todayBack;
                    rewardImage.sprite = misteriousSprite;
                    generalRewardImage.sprite = ammoSprite;
                    generalRewardValue.text = "1";
                    rewardValue.enabled = false;
                    dailyRewardObject.myReward.SetReward("Item", 1, Inventory.AMMO);
                    Inventory.Instance.AddItem(Inventory.AMMO, 1);
                    break;
                case 7:
                    rewardImage.sprite = crystalSprite;
                    generalRewardImage.sprite = crystalSprite;
                    rewardValue.text = "2";
                    generalRewardValue.text = "2";
                    rewardValue.enabled = true;
                    dailyRewardObject.myReward.SetReward("Crystals", 2);
                    GameManager.AddCrystals(2);                    
                    break;
                case 8:
                    rewardImage.sprite = crystalSprite;
                    generalRewardImage.sprite = crystalSprite;
                    rewardValue.text = "4";
                    generalRewardValue.text = "4";
                    rewardValue.enabled = true;
                    dailyRewardObject.myReward.SetReward("Crystals", 4);
                    GameManager.AddCrystals(4);
                    break;
                case 9:
                    slotBackground.sprite = todayBack;
                    rewardImage.sprite = misteriousSprite;
                    generalRewardImage.sprite = potionsSprite;
                    generalRewardValue.text = "1";
                    rewardValue.enabled = false;
                    dailyRewardObject.myReward.SetReward("Pots");
                    Inventory.Instance.AddItem(Inventory.IMMORTAL_BONUS, 1);
                    Inventory.Instance.AddItem(Inventory.DAMAGE_BONUS, 1);
                    Inventory.Instance.AddItem(Inventory.SPEED_BONUS, 1);
                    Inventory.Instance.AddItem(Inventory.TIME_BONUS, 1);
                    break;
                case 10:
                    slotBackground.sprite = skinBack;
                    rewardImage.sprite = skinSprite;
                    generalRewardImage.sprite = skinSprite;
                    generalRewardValue.text = "+ skin";
                    dailyRewardObject.myReward.SetReward("Skin");
                    rewardValue.enabled = false;

                    PlayerPrefs.SetInt("RewardDay", 0);
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (dayNum < PlayerPrefs.GetInt("RewardDay"))
            {
                rewardValue.enabled = false;
                rewardImage.sprite = collectedSprite;
                slotBackground.sprite = defaultBack;
            }
            else
            {
                switch (dayNum)
                {
                    case 1:
                        slotBackground.sprite = defaultBack;
                        rewardImage.sprite = coinSprite;
                        rewardValue.text = "100";
                        rewardValue.enabled = true;
                        break;
                    case 2:
                        slotBackground.sprite = defaultBack;
                        rewardImage.sprite = coinSprite;
                        rewardValue.text = "150";
                        rewardValue.enabled = true;
                        break;
                    case 3:
                        slotBackground.sprite = specialBack;
                        rewardImage.sprite = misteriousSprite;
                        rewardValue.enabled = false;
                        break;
                    case 4:
                        slotBackground.sprite = defaultBack;
                        rewardImage.sprite = coinSprite;
                        rewardValue.text = "200";
                        rewardValue.enabled = true;
                        break;
                    case 5:
                        slotBackground.sprite = defaultBack;
                        rewardImage.sprite = coinSprite;
                        rewardValue.text = "250";
                        rewardValue.enabled = true;
                        rewardImage.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
                        break;
                    case 6:
                        slotBackground.sprite = specialBack;
                        rewardImage.sprite = misteriousSprite;
                        rewardValue.enabled = false;
                        break;
                    case 7:
                        slotBackground.sprite = defaultBack;
                        rewardImage.sprite = crystalSprite;
                        rewardValue.text = "2";
                        rewardValue.enabled = true;
                        break;
                    case 8:
                        slotBackground.sprite = defaultBack;
                        rewardImage.sprite = crystalSprite;
                        rewardValue.text = "4";
                        rewardValue.enabled = true;
                        break;
                    case 9:
                        slotBackground.sprite = specialBack;
                        rewardImage.sprite = misteriousSprite;
                        rewardValue.enabled = false;
                        break;
                    case 10:
                        slotBackground.sprite = skinBack;
                        rewardImage.sprite = skinSprite;
                        rewardImage.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1);
                        rewardValue.enabled = false;
                        break;
                    default:
                        break;
                }
            }
        }        
    }   
}
