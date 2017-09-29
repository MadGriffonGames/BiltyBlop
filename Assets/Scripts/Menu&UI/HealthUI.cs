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
	Text healthStats;

    [SerializeField]
    private RectTransform healthbar;

    public void SetHealthbar()
    {
        healthbar.localScale = new Vector3(1  * (Player.Instance.Health / Player.Instance.maxHealth),
                                           healthbar.localScale.y,
                                           healthbar.localScale.z);
		healthStats.text = Player.Instance.Health.ToString () + "/" + Player.Instance.maxHealth.ToString (); 
		
    }
}
