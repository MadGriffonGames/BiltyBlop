using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStarterPackMap : MonoBehaviour
{
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject starterPackWindow;
    [SerializeField]
    GameObject pack1Window;
    [SerializeField]
    GameObject pack1WithNoAdsWindow;

    const string ENTER_COUNTER = "MapEnterCounter";

    private void Awake()
    {
        if (PlayerPrefs.GetInt(ENTER_COUNTER) >= 2)
        {
            PlayerPrefs.SetInt(ENTER_COUNTER, 0);

            fade.SetActive(true);
            if (PlayerPrefs.GetInt("StarterPackBought") == 0)
            {
                starterPackWindow.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("Pack1_NoAdsBought") == 0)
            {
                pack1WithNoAdsWindow.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("Pack1Bought") == 0)
            {
                pack1Window.SetActive(true);
            }
        }
    }

    bool IsBothPacksBought()
    {
        return PlayerPrefs.GetInt("StarterPackBought") > 0 && (PlayerPrefs.GetInt("Pack1_NoAdsBought") > 0 || PlayerPrefs.GetInt("Pack1Bought") > 0);
    }
}
