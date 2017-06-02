using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private static HealthUI instance;
    public static HealthUI Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<HealthUI>();
            return instance;
        }
    }

    [SerializeField]
    private Sprite[] healthSprites;

    [SerializeField]
    private RectTransform healthbar;

    float firstHBScaleX;

    private void Start()
    {
        firstHBScaleX = healthbar.localScale.x;
    }

    public void SetHealthbarDown()
    {
        if (Player.Instance.Health != 0)
        {
            healthbar.localScale = new Vector3(healthbar.localScale.x - firstHBScaleX * (1 / Player.Instance.maxHealth),
                                               healthbar.localScale.y,
                                               healthbar.localScale.z);
        }
        else
        {
            healthbar.gameObject.SetActive(false);
        }
    }

    public void SetHealthbarUp()
    {
        healthbar.localScale = new Vector3(healthbar.localScale.x + firstHBScaleX * (1 / Player.Instance.maxHealth),
                                               healthbar.localScale.y,
                                               healthbar.localScale.z);
    }
}
