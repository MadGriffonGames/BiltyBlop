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
    [SerializeField]

    public Button applyButton;
    public Button unlockButton;

    public override void Start()
    {
        //int buttonCount = buttons.Length;
        //distance = new float[buttonCount];
        //buttonDistance = (int)Mathf.Abs(buttons[1].GetComponent<RectTransform>().anchoredPosition.x - buttons[0].GetComponent<RectTransform>().anchoredPosition.x);

        //panel.anchoredPosition = new Vector2(buttons[SkinManager.Instance.NumberOfSkin(KidSkin.Instance.CurrentSkinName())].transform.position.x, panel.anchoredPosition.y);
        //minButtonsNumber = SkinManager.Instance.NumberOfSkin(KidSkin.Instance.CurrentSkinName());
        SetSkinCards();
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
                    skinCardObj.gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => ShowUnlockSkinWindow(SkinManager.Instance.NumberOfSkinPrefabBySkinOrder(skin.orderNumber)));
                    break;
                }
            }
            
        }
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

    //public void UpdateSkinModel()
    //{
    //    if (KidSkin.Instance.CurrentSkinName() != SkinManager.Instance.skinPrefabs[minButtonsNumber].name)
    //    {
    //        KidSkin.Instance.ChangeSkin(SkinManager.Instance.skinPrefabs[minButtonsNumber].name);
    //        if (!SkinManager.Instance.isSkinUnlocked(minButtonsNumber))
    //        {
    //            applyButton.gameObject.SetActive(false);
    //            unlockButton.gameObject.SetActive(true);
    //            unlockButton.GetComponentInChildren<Text>().text = KidSkin.Instance.SkinCost().ToString();
    //        }
    //        else
    //        {
    //            applyButton.gameObject.SetActive(true);
    //            unlockButton.gameObject.SetActive(false);
    //        }
    //    }
    //}

    //public override void LerpToButton(int position)
    //{
    //    float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
    //    Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
    //    panel.anchoredPosition = newPosition;

    //    if (Mathf.Abs(panel.anchoredPosition.x - position) < changingDistance)
    //    {
    //        UpdateSkinModel();
    //        tapping = false;
    //    }
    //}

}
