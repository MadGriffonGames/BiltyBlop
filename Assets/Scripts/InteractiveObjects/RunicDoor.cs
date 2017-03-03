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
			CameraEffect.Shake(0.25f, 0.25f);
			animator.SetTrigger ("open");
			player.GotKey = false;
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
