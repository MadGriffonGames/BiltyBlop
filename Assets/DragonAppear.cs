using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAppear : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start ()
    {
        SoundManager.PlaySound("breathing_fire1");
    }

	void Update ()
    {
        myRigidbody.velocity = new Vector2(12* transform.localScale.x / 1.9f, 1.1f * transform.localScale.y);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(this);
    }

}
