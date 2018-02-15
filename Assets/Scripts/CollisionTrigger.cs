using UnityEngine;
using System.Collections;

public class CollisionTrigger : MonoBehaviour
{

	[SerializeField]
	private BoxCollider2D platformCollider;

	[SerializeField]
	private BoxCollider2D platformTrigger;

	// Use this for initialization
	void Start () 
	{
		Physics2D.IgnoreCollision (platformCollider, platformTrigger, true);
	}
	
	void OnTriggerEnter2D(Collider2D other)//ignoring collisison to run trough the platform
	{
		if (other.gameObject.name == "Player" || other.gameObject.tag == "Enemy")
			Physics2D.IgnoreCollision (platformCollider, other, true);
	}

	void OnTriggerExit2D(Collider2D other)//reset collision between player and platform to make player jump on platform again
	{
		if (other.gameObject.name == "Player" || other.gameObject.tag == "Enemy")
			Physics2D.IgnoreCollision (platformCollider, other, false);
	}
}
