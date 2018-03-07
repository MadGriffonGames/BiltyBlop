using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEnterCounter
{
    const string RATE_US_ENTER_COUNTER = "AppEnterCounter";
    const string MAP_ENTER_COUNTER = "MapEnterCounter";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Debug.Log("Before first scene loaded");
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        if (PlayerPrefs.GetInt("Rated") > 0)
        {
            PlayerPrefs.SetInt(RATE_US_ENTER_COUNTER, PlayerPrefs.GetInt(RATE_US_ENTER_COUNTER) + 1);
        }

        if (PlayerPrefs.GetInt("StarterPackBought") == 0 || (PlayerPrefs.GetInt("Pack1_NoAdsBought") == 0 || PlayerPrefs.GetInt("Pack1Bought") == 0))
        {
            PlayerPrefs.SetInt(MAP_ENTER_COUNTER, PlayerPrefs.GetInt(MAP_ENTER_COUNTER) + 1);
        }
    }

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Debug.Log("RuntimeMethodLoad: After first scene loaded");
    }

}
