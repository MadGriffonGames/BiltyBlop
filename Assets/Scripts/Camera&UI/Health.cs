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

    [SerializeField]
    private Player player;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player.Health < 0)
        {
            healthUI.sprite = healthSprites[0];
        }
        else
            healthUI.sprite = healthSprites[player.Health];
	}
}
