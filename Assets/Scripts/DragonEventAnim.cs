using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEventAnim : MonoBehaviour
{
    [SerializeField]
    GameObject dragon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dragon.SetActive(true);
            dragon.GetComponent<Rigidbody2D>().velocity = new Vector3(15,0,0);
        }
    }
}
