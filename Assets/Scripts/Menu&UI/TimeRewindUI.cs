using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindUI : MonoBehaviour
{
    [SerializeField]
    GameObject fade;

    void Start ()
    {
        Time.timeScale = 0;
	}

	void Update ()
    {
		
	}
}
