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
			CameraEffect.Shake(0.25f, 0.25f);
			MyAnimator.SetTrigger ("open");
			Player.Instance.GotKey = false;
            KeyUI.Instance.KeyImage.enabled = false;
		}
	}

	public void DestroyObject()
	{
		
		Destroy(this.gameObject);
		SoundManager.PlaySound ("door_explode");
		Instantiate(stoneParticle, this.gameObject.transform.position + new Vector3(0, 0.5f , 0), Quaternion.identity);
	}

}
