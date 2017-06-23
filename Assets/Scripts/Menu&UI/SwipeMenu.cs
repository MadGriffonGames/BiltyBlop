using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{

    public RectTransform panel;
    public RectTransform center;
    public Button[] buttons;
    public Button applyButton;
    public Button unlockButton;


    private float[] distance;
    private bool dragging = false;
    private int buttonDistance;
    private int minButtonsNumber;
    private int lastDraggingButton;
    private bool onStart = true;

    private void Start()
    {
        int buttonLength = buttons.Length;
        distance = new float[buttonLength];

        buttonDistance = (int)Mathf.Abs(buttons[1].GetComponent<RectTransform>().anchoredPosition.x - buttons[0].GetComponent<RectTransform>().anchoredPosition.x);
        
        panel.anchoredPosition = new Vector2(buttons[SkinManager.Instance.NumberOfSkin(KidSkin.Instance.CurrentSkinName())].transform.position.x, panel.anchoredPosition.y);
        minButtonsNumber = SkinManager.Instance.NumberOfSkin(KidSkin.Instance.CurrentSkinName());

    }

    private void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - buttons[i].transform.position.x);
        }

        float minDistance = Mathf.Min(distance);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (minDistance == distance[i] && !onStart)
            {
                minButtonsNumber = i;
            }
        }
        if (!dragging)
        {
            LerpToButton(minButtonsNumber * -buttonDistance);
        }
    }
    void LerpToButton(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;
        onStart = false;

    }
    public void EndDrag()
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
        dragging = false;
    }



}
