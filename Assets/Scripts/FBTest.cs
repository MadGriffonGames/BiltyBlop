using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using System.Collections.Generic;

class FBTest : MonoBehaviour
{
    [SerializeField]
    GameObject testTimer;

    private void FixedUpdate()
    {
        testTimer.GetComponent<Text>().text = "";
        testTimer.GetComponent<Text>().text += PlayerPrefs.GetInt("Crystals");
    }
}

