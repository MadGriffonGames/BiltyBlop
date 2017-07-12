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
        minButtonsNumber = 1;
        OnButtonClickLerp(minButtonsNumber);
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
        if (minButtonsNumber == 0 && tapping)
        {
            MakeActiveButton(0);
            minButtonsNumber = 1;
        }
        else if (minButtonsNumber == buttons.Length - 1 && tapping)
        {
            MakeActiveButton(minButtonsNumber);
            minButtonsNumber = buttons.Length - 2;
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - buttons[i].transform.position.x);
        }

        float minDistance = Mathf.Min(distance);
        if (!tapping)
        {
            for (int i = 1; i < buttons.Length - 1; i++)
            {
                if (minDistance == distance[i] && !onStart)
                {
                    minButtonsNumber = i;
                }
            }
        }
        if (!dragging || tapping)
        {
            LerpToButton(minButtonsNumber * -buttonDistance);
        }
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
    }

    public override void OnButtonClickLerp(int buttonNumber)
    {
        if (buttonNumber == 0)
        {
            minButtonsNumber = 1;
        }
        else if (buttonNumber == buttons.Length - 1)
        {
            minButtonsNumber = buttons.Length - 2;
        } else
        {
            minButtonsNumber = buttonNumber;
        }
        MakeActiveButton(buttonNumber);
        tapping = true;
    }


    public void MakeActiveButton(int buttonNumber)
    {
        buttons[buttonNumber].gameObject.transform.localScale = increasedButttonScale;
        buttons[buttonNumber].GetComponentsInChildren<Text>()[1].enabled = true;
        MakeOtherButtonsInactive(buttonNumber);
    }
    public void MakeInactiveButton(int buttonNumber)
    {
        buttons[buttonNumber].gameObject.transform.localScale = normalButttonScale;
        buttons[buttonNumber].GetComponentsInChildren<Text>()[1].enabled = false;
    }
    private void MakeOtherButtonsInactive(int activeButton)
    {
        for (int i = 0; i < buttons.Length; i++)
        {

            if (i != activeButton)
            {
                MakeInactiveButton(i);
                Debug.Log(i);
            }
        }
    }

    public void UnlockPerk()
    {
        PlayerPrefs.SetString(buttons[minButtonsNumber].GetComponentInChildren<Text>().text, "Unlocked");
        // need to add payment for perks
    }
}
