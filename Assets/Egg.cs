using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    [SerializeField]
    GameObject crashedEgg;
    [SerializeField]
    GameObject eggParticle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            gameObject.SetActive(false);
            Instantiate(eggParticle, this.gameObject.transform.position + new Vector3(0, 1f), Quaternion.identity);
            crashedEgg.gameObject.SetActive(true);
            SoundManager.PlaySound("egg_crack");
        }
    }
}
