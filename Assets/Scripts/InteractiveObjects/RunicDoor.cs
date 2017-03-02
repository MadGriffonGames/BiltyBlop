using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunicDoor : InteractiveObject {

	[SerializeField]
	GameObject stoneParticle;

	public override void Start ()
	{
		base.Start();
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player") && Player.Instance.GotKey)
		{
			Player.Instance.GotKey = false;
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
