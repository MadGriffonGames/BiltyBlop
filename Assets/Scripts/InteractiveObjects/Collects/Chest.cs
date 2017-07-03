using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractiveObject
{

    private bool isEmpty;

	[SerializeField]
	private GameObject[] coins;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        isEmpty = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEmpty && other.transform.CompareTag("Sword"))
        {
			MyAnimator.SetTrigger ("open");
            SoundManager.PlaySound("chest open");
            for (int i = 0; i < 10; i++)
            {
                coins[i].SetActive(true);
                coins[i].GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(7f, 9f));
            }
            isEmpty = true;
        }
    }
}
