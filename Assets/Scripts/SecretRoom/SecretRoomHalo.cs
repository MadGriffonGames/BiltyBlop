using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomHalo : MonoBehaviour
{
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
