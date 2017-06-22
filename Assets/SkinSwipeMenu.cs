using UnityEngine;
using UnityEngine.UI;

public class SkinSwipeMenu : SwipeMenu {


    public Button applyButton;
    public Button unlockButton;

    public override void Start()
    {
        int buttonCount = buttons.Length;
        distance = new float[buttonCount];
        buttonDistance = (int)Mathf.Abs(buttons[1].GetComponent<RectTransform>().anchoredPosition.x - buttons[0].GetComponent<RectTransform>().anchoredPosition.x);

        panel.anchoredPosition = new Vector2(buttons[SkinManager.Instance.NumberOfSkin(KidSkin.Instance.CurrentSkinName())].transform.position.x, panel.anchoredPosition.y);
        minButtonsNumber = SkinManager.Instance.NumberOfSkin(KidSkin.Instance.CurrentSkinName());
    }

    public void UpdateSkinModel()
    {
        if (KidSkin.Instance.CurrentSkinName() != SkinManager.Instance.skinPrefabs[minButtonsNumber].name)
        {
            KidSkin.Instance.ChangeSkin(SkinManager.Instance.skinPrefabs[minButtonsNumber].name);
            if (!SkinManager.Instance.isSkinUnlocked(minButtonsNumber))
            {
                applyButton.gameObject.SetActive(false);
                unlockButton.gameObject.SetActive(true);
                unlockButton.GetComponentInChildren<Text>().text = KidSkin.Instance.SkinCost().ToString();
            }
            else
            {
                applyButton.gameObject.SetActive(true);
                unlockButton.gameObject.SetActive(false);
            }
        }
    }

    public override void LerpToButton(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
        panel.anchoredPosition = newPosition;

        if (Mathf.Abs(panel.anchoredPosition.x - position) < changingDistance)
        {
            UpdateSkinModel();
            tapping = false;
        }
    }

}
