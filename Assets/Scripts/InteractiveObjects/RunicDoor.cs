using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunicDoor : InteractiveObject {

	[SerializeField]
	GameObject stoneParticle;

	// Use this for initialization
	public override void Start ()
	{
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && player.GotKey)
		{
			player.GotKey = false;
			KeyUI.Instance.KeyImage.enabled = false;
			animator.SetTrigger ("open");
			SoundManager.MusicVolume (2f);
			SoundManager.PlaySound ("key_enter");

		}
	}

	public void DestroyObject()
	{
		Destroy(this.gameObject);
		CameraEffect.Shake(0.4f, 0.4f);
		Instantiate(stoneParticle, this.gameObject.transform.position + new Vector3(0, 0.5f , 0), Quaternion.identity);
		SoundManager.PlaySound ("door_explode");
	}

}
