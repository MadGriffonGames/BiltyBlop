using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class EvilFlower : MonoBehaviour 

{
	UnityArmatureComponent armature;

    void Awake()
    {
		armature = GetComponent<UnityArmatureComponent> ();
    }

	public void Idle()
	{
        armature.animation.timeScale = 1f;
        armature.animation.Play ("IDLE");
	}

	public void Attack()
	{
		armature.animation.timeScale = 1.5f;
		armature.animation.Play ("ATTACK");

	}

    public void Preparation()
    {
        armature.animation.timeScale = 2f;
        armature.animation.Play("PREPARATION");
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
