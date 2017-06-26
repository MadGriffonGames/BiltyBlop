using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PerksSwipeMenu : SwipeMenu {

    private int previousActiveButton;
    private static Vector3 normalButttonScale = new Vector3(1, 1, 1);
    private static Vector3 increasedButttonScale = new Vector3(1.2f, 1.2f, 1);

    public Button unlockButton;
    // Use this for initialization
    public override void Start ()
    {
        minButtonsNumber = 2;
        ObButtonClickLerp(minButtonsNumber);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (!PlayerPrefs.HasKey(buttons[i].GetComponentInChildren<Text>().text))
            {
                PlayerPrefs.SetString(buttons[i].GetComponentInChildren<Text>().text, "Locked");
            }
        }
        base.Start();
	}

    public override void Update()
    {
        base.Update();
        if (PlayerPrefs.GetString(buttons[minButtonsNumber].GetComponentInChildren<Text>().text) == "Unlocked")
        {
            unlockButton.gameObject.SetActive(false);
        }
        else
            unlockButton.gameObject.SetActive(true);
    }

    public override void LerpToButton(int position)
    {
        base.LerpToButton(position);
        MakeInctiveButton(previousActiveButton);
        MakeActiveButton(minButtonsNumber);
        previousActiveButton = minButtonsNumber;
    }

    public override void ObButtonClickLerp(int buttonNumber)
    {
        base.ObButtonClickLerp(buttonNumber);

        MakeInctiveButton(previousActiveButton);
        MakeActiveButton(buttonNumber);
        previousActiveButton = buttonNumber;
    }


    public void MakeActiveButton(int buttonNumber)
    {
        buttons[buttonNumber].gameObject.transform.localScale = increasedButttonScale;
        buttons[buttonNumber].GetComponentInChildren<Text>().enabled = true;
    }
    public void MakeInctiveButton(int buttonNumber)
    {
        buttons[buttonNumber].gameObject.transform.localScale = normalButttonScale;
        buttons[buttonNumber].GetComponentInChildren<Text>().enabled = false;
    }

    public void UnlockPerk()
    {
        PlayerPrefs.SetString(buttons[minButtonsNumber].GetComponentInChildren<Text>().text, "Unlocked");
        // need to add payment for perks
    }
}
