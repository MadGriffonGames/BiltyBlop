﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    GameObject nextPortal;
    GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mainCamera.transform.position = nextPortal.transform.position + new Vector3(0, 0, -20);
            Player.Instance.transform.position = nextPortal.transform.position + new Vector3(1 * Player.Instance.transform.localScale.x, -2f, 0);
        }
    }
}
