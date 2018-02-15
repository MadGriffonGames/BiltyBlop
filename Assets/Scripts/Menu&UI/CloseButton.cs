using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField]
    GameObject window;
    [SerializeField]
    GameObject fade;

    public void CloseWindow()
    {
        window.SetActive(false);
        if (fade != null)
        {
            fade.SetActive(false);
        }
    }
}
