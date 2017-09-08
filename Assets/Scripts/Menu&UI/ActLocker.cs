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

    bool collectStars = false;
    bool completeLevel = false;

	void Start ()
    {
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
            this.gameObject.SetActive(false);
            statPanel.SetActive(true);
        }
        else
        {
            transform.parent.GetComponent<Button>().enabled = false;
            statPanel.SetActive(false);
        }
	}
}
