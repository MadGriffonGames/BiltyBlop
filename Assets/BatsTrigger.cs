using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class BatsTrigger : MonoBehaviour {

    [SerializeField]
    GameObject Bats;

    bool wasUsed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!wasUsed)
        if (other.gameObject.CompareTag("Player"))
        {
                Enemy[] bats = Bats.GetComponentsInChildren<Enemy>();
                foreach(Enemy bat in bats)
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
