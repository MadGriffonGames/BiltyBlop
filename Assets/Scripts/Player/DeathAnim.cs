using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnim : MonoBehaviour
{
    public void InstantiateGrave()
    {
        Player.Instance.InstantiateGrave();
    }

    public void SwitchOnDeathUI()
    {
        UI.Instance.timeRewindUI.SetActive(true);
    }
}
