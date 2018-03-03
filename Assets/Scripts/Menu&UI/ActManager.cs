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
    GameObject episode3;
    [SerializeField]
	GameObject actSpacer;
    [SerializeField]
    GameObject mapButtons;

	public void TurnOnEoisode1(bool turn)
	{
		actSpacer.SetActive (!turn);
		episode1.SetActive (turn);
        mapButtons.SetActive(true);
	}

    public void TurnOnEpisode2(bool turn)
    {
        actSpacer.SetActive(!turn);
        episode2.SetActive(turn);
        mapButtons.SetActive(true);
    }

    public void TurnOnEpisode3(bool turn)
    {
        actSpacer.SetActive(!turn);
        episode3.SetActive(turn);
        mapButtons.SetActive(true);
    }
}
