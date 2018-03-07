using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEventAnim : MonoBehaviour
{
    [SerializeField]
    GameObject dragon;
    [SerializeField]
    GameObject secondPoint;
    bool isUp;


    private void Update()
    {
        if (dragon.GetComponent<Transform>().position.x >= secondPoint.GetComponent<Transform>().position.x && !isUp)
        {
            isUp = true;
            dragon.GetComponent<Rigidbody2D>().velocity = new Vector3(15, 4, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dragon.SetActive(true);
            dragon.GetComponent<Rigidbody2D>().velocity = new Vector3(15,0,0);
        }
    }

    private void OnBecameInvisible()
    {
        dragon.SetActive(false);
        Destroy(dragon);
    }
}
