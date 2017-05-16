using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class BatsTrigger : MonoBehaviour {

    [SerializeField]
    GameObject Bats;

    bool wasUsed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!wasUsed)
        if (other.gameObject.CompareTag("Player"))
        {
                Bat[] bats = Bats.GetComponentsInChildren<Bat>();
                foreach(Bat bat in bats)
                {
                    bat.enabled = true;
                    bat.MyAniamtor.enabled = true;
                    UnityArmatureComponent armature = bat.gameObject.GetComponent<UnityArmatureComponent>();
                    armature.enabled = true;
                }

                wasUsed = true;
        }
    }
}
