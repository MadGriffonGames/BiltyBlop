using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActLocker : MonoBehaviour
{
    [SerializeField]
    int unlockStarsCount;
    [SerializeField]
    int unlockLevel;
    [SerializeField]
    Text unlockNumber;
    [SerializeField]
    GameObject levelPanel;
    [SerializeField]
    GameObject starsPanel;
    [SerializeField]
    GameObject statPanel;
    const int TOTAL_LEVEL_COUNT = 20;

    bool collectStars = false;
    bool completeLevel = false;

    int starsCount = 0;

	void Start ()
    {
        for (int i = 0; i <= TOTAL_LEVEL_COUNT; i++)
        {
            starsCount += PlayerPrefs.GetInt("Level" + i + "_collects");
        }
        //DELETE BEFORE RELEASE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        starsCount = 35;

        PlayerPrefs.SetInt("GeneralStarsCount", starsCount);

        unlockNumber.text = unlockStarsCount.ToString();

        if (unlockStarsCount <= PlayerPrefs.GetInt("GeneralStarsCount"))
        {
            collectStars = true;
            starsPanel.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Level" + unlockLevel) > 0)
        {
            completeLevel = true;
            levelPanel.SetActive(false);
        }

        if (collectStars && completeLevel)
        {
            statPanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            transform.parent.GetComponent<Button>().enabled = false;
            statPanel.SetActive(false);
        }
	}
}
