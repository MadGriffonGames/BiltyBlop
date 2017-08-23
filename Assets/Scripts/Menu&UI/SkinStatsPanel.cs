using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinStatsPanel : MonoBehaviour {

	[SerializeField]
	Text hpStatText;
	[SerializeField]
	Text damageStatText;

    public void SetDefendIndicators(int stat)
    {      
		hpStatText.text = (3 + stat).ToString();   
    }

    public void SetAttackIndicators(int stat)
    {
		damageStatText.text = (1 + stat).ToString ();
    }

}
