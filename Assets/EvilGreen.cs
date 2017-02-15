using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class EvilGreen : MonoBehaviour {

	// Use this for initialization
	void Awake()
	{
		// Load data.
		UnityFactory.factory.LoadDragonBonesData("EvilFlowerGreen"); // DragonBones file path (without suffix)
		UnityFactory.factory.LoadTextureAtlasData("EvilFlowerGreen"); //Texture atlas file path (without suffix) 
		// Create armature.
		var armatureComponent =UnityFactory.factory.BuildArmatureComponent("EvilFlowerGreen"); // Input armature name
		// Play animation.
		armatureComponent.animation.Play("IDLE");

		// Change armatureposition.
		armatureComponent.transform.localPosition = new Vector3(0.0f, 0.0f,0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
