using UnityEngine;
using UnityEngine.UI;

public class SkinSwipeMenu : SwipeMenu {

    [SerializeField]
    GameObject skinCard;
    [SerializeField]
    GameObject unlockSkinWindow;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject closeBuyWindowButton;

    public override void Start()
    {
        SetSkinCards();
        buttons = new GameObject[panel.transform.childCount];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = panel.GetChild(i).transform.gameObject;
        }
        
        distance = new float[buttons.Length];
        buttonDistance = (int)Mathf.Abs(buttons[1].GetComponent<RectTransform>().anchoredPosition.x - buttons[0].GetComponent<RectTransform>().anchoredPosition.x);

        panel.anchoredPosition = new Vector2(buttons[1].transform.position.x, panel.anchoredPosition.y);
        minButtonsNumber = 1;
    }

    public override void Update()
    {
        base.Update();
    }
    private void SetSkinCards()
    {
        for (int i = 0; i < SkinManager.Instance.skinPrefabs.Length; i++)
        {
            for (int j = 0; j < SkinManager.Instance.skinPrefabs.Length; j++)
            {
                if (SkinManager.Instance.skinPrefabs[j].GetComponent<SkinPrefab>().orderNumber == i)
                {
                    
                    GameObject skinCardObj = Instantiate(skinCard) as GameObject;
                    SkinPrefab skin = SkinManager.Instance.skinPrefabs[j].GetComponent<SkinPrefab>();

                    skinCardObj.transform.SetParent(panel);
                    skinCardObj.transform.localScale = new Vector3(1, 1, 1);
                    skinCardObj.gameObject.GetComponentsInChildren<Text>()[0].text = skin.shopName;
                    skinCardObj.gameObject.GetComponentsInChildren<Image>()[1].sprite = skin.skinSprite;
                    if (!skin.isLocked)
                    {
                        if (PlayerPrefs.GetString("Skin") == skin.name)
                        {
                            skinCardObj.gameObject.GetComponentsInChildren<Button>()[1].GetComponentInChildren<Text>().text = "EQUIPED";
                        }
                        else
                            skinCardObj.gameObject.GetComponentsInChildren<Button>()[1].GetComponentInChildren<Text>().text = "EQUIP";
                        skinCardObj.gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => ApplySkin(skin.orderNumber));
                    }
                    else
                    {
                        skinCardObj.gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => ShowUnlockSkinWindow(SkinManager.Instance.NumberOfSkinPrefabBySkinOrder(skin.orderNumber)));
                    }
                    break;
                }
            }
            
        }
    }

    public void ApplySkin(int skinOrderNumber) // writing to player prefs current skin
    {
        panel.GetChild(skinOrderNumber).GetComponentsInChildren<Text>()[1].text = "EQUIPED";
        for (int i = 0; i < panel.childCount; i++)
        {
            if (i != skinOrderNumber && !SkinManager.Instance.isSkinLocked(SkinManager.Instance.NumberOfSkinPrefabBySkinOrder(i)))
            {
                panel.GetChild(i).GetComponentsInChildren<Text>()[1].text = "EQUIP";
            }
        }
        SkinManager.Instance.ApplySkin(SkinManager.Instance.NameOfSkinPrefabBySkinOrder(skinOrderNumber));
    }

    public void ShowUnlockSkinWindow(int skinNumber)
    {
        unlockSkinWindow.gameObject.SetActive(true);
        fade.gameObject.SetActive(true);
        closeBuyWindowButton.gameObject.SetActive(true);

        unlockSkinWindow.GetComponent<UnlockSkinWindow>().SetWindowWithSkinNumber(skinNumber);
    }
    public void CloseUnlockSkinWindow()
    {
        unlockSkinWindow.gameObject.SetActive(false);
        fade.gameObject.SetActive(false);
        closeBuyWindowButton.gameObject.SetActive(false);
    }

    public override void LerpToButton(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
        panel.anchoredPosition = newPosition;

        if (Mathf.Abs(panel.anchoredPosition.x - position) < changingDistance)
        {
            tapping = false;
        }
    }

}
