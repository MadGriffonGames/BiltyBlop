using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer mainCoin;
    SpriteRenderer MySR;

	void Start ()
    {
        MySR = GetComponent<SpriteRenderer>();
        enabled = false;
	}

	void Update ()
    {
        MySR.sprite = mainCoin.sprite;
	}

    private void OnBecameVisible()
    {
        enabled = true;
    }
}
