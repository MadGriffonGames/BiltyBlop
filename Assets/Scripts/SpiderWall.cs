using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWall : InteractiveObject {
    [SerializeField]
    public GameObject groundParticle;

    // Use this for initialization
    void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Sword") || other.transform.CompareTag("Throwing"))
        {
            CameraEffect.Shake(0.2f, 0.2f);
            Instantiate(groundParticle, this.gameObject.transform.position + new Vector3(0, 0.5f, -3), Quaternion.identity);
            SoundManager.PlaySound("wooden_box1");
            Destroy(this.gameObject);
        }
    }
}
