using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManager : MonoBehaviour
{
	[SerializeField]
	GameObject episode1;
    [SerializeField]
    GameObject episode2;
	[SerializeField]
	GameObject actSpacer;

	public void TurnOnEoisode1(bool turn)
	{
		actSpacer.SetActive (!turn);
		episode1.SetActive (turn);
	}

    public void TurnOnEpisode2(bool turn)
    {
        actSpacer.SetActive(!turn);
        episode2.SetActive(turn);
    }
}
