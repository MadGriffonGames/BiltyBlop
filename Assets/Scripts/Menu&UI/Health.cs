using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Sprite[] healthSprites;

    [SerializeField]
    private Image healthUI;

	void Update ()
    {
        if (Player.Instance.Health < 0)
        {
            healthUI.sprite = healthSprites[0];
        }
        else
            healthUI.sprite = healthSprites[Player.Instance.Health];
	}
}
