using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinStatsPanel : MonoBehaviour
{
	[SerializeField]
	Text hpStatText;
	[SerializeField]
	Text coinCostText;
	[SerializeField]
	Text attackStatText;
	[SerializeField]
	GameObject coin;
	[SerializeField]
	GameObject check;
	[SerializeField]
	GameObject statHalo;

    public void SetDefendIndicators(int stat)
    {      
		hpStatText.text = stat.ToString();
    }

    public void SetCoinCost(int stat)
    {
		coinCostText.text = stat.ToString();
    }
	public void SetAttackIndicators(int stat)
	{
		attackStatText.text = stat.ToString();
	}
	public void TurnOffCoinCost()
	{
		coin.SetActive (false);
		coinCostText.text = "";
	}
	public void ActivateCheck(bool active)
	{
		check.SetActive (active);
	}
	public void HighliteStat()
	{
		statHalo.SetActive (true);
		ActivateCheck (false);
		TurnOffCoinCost ();
		this.gameObject.transform.localPosition = new Vector3 (0,this.gameObject.transform.localPosition.y,this.gameObject.transform.localPosition.z);
	}
}
