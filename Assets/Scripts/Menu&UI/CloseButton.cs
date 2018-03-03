using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField]
    GameObject window;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject[] otherObjects;

    public void CloseWindow()
    {
        window.SetActive(false);
        for (int i = 0; i < otherObjects.Length; i++)
        {
            otherObjects[i].SetActive(false);
        }

        if (fade != null)
        {
            fade.SetActive(false);
        }
    }
}
