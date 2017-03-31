using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPack : MonoBehaviour
{
    [SerializeField]
    GameObject[] coins;

    void Start ()
    {
        for (int i = 0; i < coins.Length; i++)
            coins[i].SetActive(false);

	}

    private void OnBecameVisible()
    {
        for (int i = 0; i < coins.Length; i++)
            coins[i].SetActive(true);
    }
}
