using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Snowball : MonoBehaviour
{

    public Vector3 startPosition;

    [SerializeField]
    public GameObject particle;

    private Vector2 direction;

    //----------------------------------------
    private void Start()
    {
        //Throw(transform.position, new Vector2(60, 15));
    }
    //----------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("GroundYeti") || other.transform.CompareTag("Sword"))
        {
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            this.gameObject.SetActive(false);
            SoundManager.PlaySound("stone_crash");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("GroundYeti") || other.transform.CompareTag("Sword"))
        {
            SoundManager.PlaySound("stone_crash");
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0, -0.7f, 0), Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }

    public void Throw(Vector3 startPos, Vector2 power)
    {
        this.transform.position = startPos;
        gameObject.SetActive(true);
        this.GetComponent<Rigidbody2D>().velocity += power;
    }
}
