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

	private string crystalsImagePath = "Sprites/UI/Loot/Crystals/CrystalMiddlePack";
	private Sprite crystalsSprite;


    public void SetDefendIndicators(int stat)
    {      
		hpStatText.text = stat.ToString();
    }

    public void SetCoinCost(int stat)
    {
		coinCostText.text = stat.ToString();
    }

	public void SetCrystalCost(int stat)
	{
		coinCostText.text = stat.ToString();
		ChangeCoinPictureToCrystals ();
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
	public void ChangeCoinPictureToCrystals()
	{
		crystalsSprite = Resources.Load<Sprite>(crystalsImagePath);
		coin.GetComponent<Image> ().sprite = crystalsSprite;
		coin.transform.localScale = new Vector3 (1.5f, 1.5f, 1f);
	}
}
