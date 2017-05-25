using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    [SerializeField]
    private Enemy enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && Player.Instance.Health != 0)
        {
            enemy.Target = other.gameObject;
        }
        if (other.tag == "grave")
        {
            enemy.Target = null;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "grave" && Player.Instance.Health == 0)
        {
            enemy.Target = null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.Target = null;
        }
    }
}
