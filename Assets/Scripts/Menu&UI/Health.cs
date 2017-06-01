using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Sprite[] healthSprites;

    [SerializeField]
    private RectTransform healthbar;

    float firstHBScaleX;
    int maxHealth;

    private void Start()
    {
        maxHealth = Player.Instance.Health;
        firstHBScaleX = healthbar.localScale.x;
        Debug.Log(1 / maxHealth);
    }

    void Update ()
    {
        if (Player.Instance.takeHit)
        {
            SetHealthbar();
        }
	}

    public void SetHealthbar()
    {
        if (Player.Instance.Health != 0)
        {
            healthbar.localScale = new Vector3(healthbar.localScale.x - firstHBScaleX * (1 / maxHealth),
                                               healthbar.localScale.y,
                                               healthbar.localScale.z);
        }
        
    }
}
