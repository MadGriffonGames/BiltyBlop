using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public Text levelText;
    public int unlocked;
    [SerializeField]
    public GameObject[] stars;
    [SerializeField]
    GameObject Lock;
    RectTransform MyRectTransfrom;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Level" + levelText.text + "_collects"))
        {
            ShowStars(PlayerPrefs.GetInt("Level" + levelText.text + "_collects"));
        }
        if (unlocked == 0)
        {
            Lock.SetActive(true);
        }
        else
        {
            Lock.SetActive(false);
        }
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
}
