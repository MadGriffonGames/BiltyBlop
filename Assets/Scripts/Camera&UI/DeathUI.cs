using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    [SerializeField]
    GameObject controls;

    void Start ()
    {
        this.gameObject.SetActive(false);
        controls.SetActive(false);
	}

	void Update ()
    {
		
	}
}
