using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManager : MonoBehaviour {

	[SerializeField]
	GameObject spacer;
	[SerializeField]
	GameObject actSpacer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TurnOnLevelSelect(bool turn)
	{
		actSpacer.SetActive (!turn);
		spacer.SetActive (turn);
	}
}
