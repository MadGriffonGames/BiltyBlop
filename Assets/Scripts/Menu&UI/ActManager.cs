using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManager : MonoBehaviour
{
	[SerializeField]
	GameObject map;
	[SerializeField]
	GameObject actSpacer;

	public void TurnOnLevelSelect(bool turn)
	{
		actSpacer.SetActive (!turn);
		map.SetActive (turn);
	}
}
