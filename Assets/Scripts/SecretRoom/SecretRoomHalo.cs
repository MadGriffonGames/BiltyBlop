using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomHalo : MonoBehaviour
{
    public void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.secretIndication.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.secretIndication.SetActive(false);
        }
    }
}
