using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject secretRoom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            secretRoom.SetActive(false);
        }
    }

}
