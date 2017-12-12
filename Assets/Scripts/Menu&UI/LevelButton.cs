using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public string levelText;
    public int unlocked;
    [SerializeField]
    public GameObject[] stars;
    [SerializeField]
    GameObject Lock;
    RectTransform MyRectTransfrom;
    bool isInteractable = false;

    private void Start()
    {
        SetButton();
        
        if (unlocked == 0)
        {
            Lock.SetActive(true);
        }
        else
        {
            if (IsItLastUnlockedLevel("Level" + levelText))
            {
                int tmp = int.Parse(levelText) - 1;
                if (PlayerPrefs.GetString("LastCompletedLevel") == "Level" + tmp.ToString())
                {
                    Lock.SetActive(true);
                    Lock.GetComponent<Animator>().enabled = true;
                }
            }
            else
            {
                Lock.SetActive(false);
            }
            if (PlayerPrefs.HasKey("Level" + levelText + "_collects"))
            {
                ShowStars(PlayerPrefs.GetInt("Level" + levelText + "_collects"));
            }
        }

    }

    void SetButton()
    {
        if (PlayerPrefs.GetInt("Level" + levelText) == 1)
        {
            unlocked = 1;
            isInteractable = true;
        }
        else
        {
            unlocked = 0;
        }
        GetComponent<Button>().interactable = isInteractable;
    }

    public void ShowStars(int value)
    {
        for (int i = 0; i < value; i++)
        {
            if (value <= 3)
            {
                stars[i].SetActive(true);
            }
        }
    }

    public void HideStars()
    {
        for (int i = 0; i < 3; i++)
        {
            stars[i].SetActive(false);
        }
    }

    bool IsItLastUnlockedLevel(string levelName)
    {
        return PlayerPrefs.GetString("LastUnlockedLevel") == levelName;
    }

}
