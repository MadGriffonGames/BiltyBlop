using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Advertisements;

public class AdsChest : MonoBehaviour, IAdsPlacement
{
    public const int COINS_RANGE = 50;
    public const int CRYSTALS_RANGE = 75;
    public const int ITEMS_RANGE = 100;

    public const int MIN_COIN_RANGE = 50;
    public const int MID_COIN_RANGE = 70;
    public const int BIG_COIN_RANGE = 100;

    public const int MIN_CRYSTAL_RANGE = 65;
    public const int MID_CRYSTAL_RANGE = 85;
    public const int BIG_CRYSTAL_RANGE = 100;

    public const int ITEMS_COUNT = 6;

    public const int HEAL_NUM = 0;
    public const int AMMO_NUM = 1;
    public const int IMMORTAL_NUM = 2;
    public const int DAMAGE_NUM = 3;
    public const int SPEED_NUM = 4;
    public const int TIME_NUM = 5;

    float currentTime;

    [SerializeField]
    Sprite[] lootArray;
    [SerializeField]
    public SpriteRenderer loot;
    [SerializeField]
    Sprite openChest;
    bool isOpened;
    bool isRewardCollected;
    bool musicWasPlaying;

    public int[] itemsDropRate;
    public int[] itemsStorage;

    float memTimeScale;

    EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    public void Start()
    {
        isOpened = false;
        isRewardCollected = false;

        itemsDropRate = new int[ITEMS_COUNT];
        itemsStorage = new int[100];

        if (!PlayerPrefs.HasKey("LevelAdsChest"))
        {
            PlayerPrefs.SetInt("LevelAdsChest", 0);
        }

        SetItems();
    }

    public void Randomize()
    {
        if (PlayerPrefs.GetInt("LevelAdsChest") == 0)
        {
            PlayerPrefs.SetInt("LevelAdsChest", 1);

            loot.sprite = lootArray[1];
            AddCrystals(4);
        }
        else
        {
            int random = UnityEngine.Random.Range(1, 100);

            if (random <= COINS_RANGE)
            {
                RandomizeCoins();
            }
            if (random > COINS_RANGE && random <= CRYSTALS_RANGE)
            {
                RandomizeCrystals();
            }
            if (random > CRYSTALS_RANGE && random <= ITEMS_RANGE)
            {
                RandomizeItems();
            }
        }     
    }

    void RandomizeCoins()
    {
        int random = UnityEngine.Random.Range(1, 100);

        if (random <= MIN_COIN_RANGE)
        {
            loot.sprite = lootArray[0];
            AddCoins(25);
        }
        if (random > MIN_COIN_RANGE && random <= MID_COIN_RANGE)
        {
            loot.sprite = lootArray[0];
            AddCoins(50);
        }
        if (random > MID_COIN_RANGE && random <= BIG_COIN_RANGE)
        {
            loot.sprite = lootArray[0];
            AddCoins(100);
        }
    }

    void RandomizeCrystals()
    {
        int random = UnityEngine.Random.Range(1, 100);

        if (random <= MIN_CRYSTAL_RANGE)
        {
            loot.sprite = lootArray[1];
            AddCrystals(2);
        }
        if (random > MIN_CRYSTAL_RANGE && random <= MID_CRYSTAL_RANGE)
        {
            loot.sprite = lootArray[1];
            AddCrystals(3);
        }
        if (random > MID_CRYSTAL_RANGE && random <= BIG_CRYSTAL_RANGE)
        {
            loot.sprite = lootArray[1];
            AddCrystals(4);
        }
    }

    void RandomizeItems()
    {
        int random = UnityEngine.Random.Range(1, 100);

        switch (itemsStorage[random])
        {
            case 0:
                AddItem(Inventory.HEAL, itemsStorage[random] + 2);//2 - позиция первого итема в массиве lootArray
                break;

            case 1:
                AddItem(Inventory.AMMO, itemsStorage[random] + 2);//2 - позиция первого итема в массиве lootArray
                break;

            case 2:
                AddItem(Inventory.IMMORTAL_BONUS, itemsStorage[random] + 2);//2 - позиция первого итема в массиве lootArray
                break;

            case 3:
                AddItem(Inventory.DAMAGE_BONUS, itemsStorage[random] + 2);//2 - позиция первого итема в массиве lootArray
                break;

            case 4:
                AddItem(Inventory.SPEED_BONUS, itemsStorage[random] + 2);//2 - позиция первого итема в массиве lootArray
                break;

            case 5:
                AddItem(Inventory.TIME_BONUS, itemsStorage[random] + 2);//2 - позиция первого итема в массиве lootArray
                break;

            default:
                break;
        }
    }

    void AddCoins(int value)
    {
        loot.gameObject.GetComponentInChildren<TextMesh>().text = value.ToString();
        GameManager.collectedCoins += value;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + value);
    }

    void AddCrystals(int value)
    {
        loot.gameObject.GetComponentInChildren<TextMesh>().text = value.ToString();
        PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + value);
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            GameManager.crystalTxt.text = PlayerPrefs.GetInt("Crystals").ToString();
        }
    }

    void AddItem(string itemName, int itemNum)
    {
        loot.gameObject.GetComponentInChildren<TextMesh>().text = "+1";
        loot.sprite = lootArray[itemNum];
        Inventory.Instance.AddItem(itemName, 1);
    }

    public void SetItems()
    {
        SetItemsWeights();
        SetItemsDropRate();
    }

    public void SetItemsWeights()
    {
        itemsDropRate[HEAL_NUM] = 30;
        itemsDropRate[AMMO_NUM] = 30;
        itemsDropRate[IMMORTAL_NUM] = 5;
        itemsDropRate[DAMAGE_NUM] = 10;
        itemsDropRate[SPEED_NUM] = 15;
        itemsDropRate[TIME_NUM] = 10;
    }

    public void SetItemsDropRate()
    {
        int lastItemIndex = 0;

        for (int itemNum = 0; itemNum < ITEMS_COUNT; itemNum++)//заполняем массив idшками итемов, в соответствии с их дроп-рейтом
        {
            for (int j = lastItemIndex; j < lastItemIndex + itemsDropRate[itemNum]; j++)//пишем id итема в storage столько раз, сколько его дроп-рэйт
            {
                itemsStorage[j] = itemNum;
                if (j + 1 == lastItemIndex + itemsDropRate[itemNum])
                {
                    lastItemIndex = j + 1;
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword") && !isRewardCollected)
        {
            isOpened = true;
            Player.Instance.ChangeState(new PlayerIdleState());

            AppMetrica.Instance.ReportEvent("#ADS_CHEST opened in " + GameManager.currentLvl);
            DevToDev.Analytics.CustomEvent("#ADS_CHEST opened in " + GameManager.currentLvl);

            eventSystem.enabled = false;
            StartCoroutine(EnableEventSystem());
            AdsManager.Instance.ShowRewardedVideo(this);
        }
    }

    protected void EnableControls(bool switcher)
    {
        if (switcher)
        {
            UI.Instance.controlsUI.SetActive(true);
        }
        else
        {
            UI.Instance.controlsUI.SetActive(false);
        }
    }

    IEnumerator EnableEventSystem()
    {
        yield return new WaitForSeconds(1);
        if (!eventSystem.enabled)
        {
            eventSystem.enabled = true;
        }
        
    }

    public void OnRewardedVideoWatched()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        isRewardCollected = true;

        AppMetrica.Instance.ReportEvent("#ADS_CHEST_VIDEO watched in " + GameManager.currentLvl);
        DevToDev.Analytics.CustomEvent("#ADS_CHEST_VIDEO watched in " + GameManager.currentLvl);

        Randomize();
        GetComponent<SpriteRenderer>().sprite = openChest;
        loot.gameObject.SetActive(true);

        Player.Instance.ChangeState(new PlayerIdleState());
    }

    public void OnRewardedVideoFailed()
    {
        Player.Instance.ChangeState(new PlayerIdleState());
        isOpened = false;
    }
}
