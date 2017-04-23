using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject secretRoom;
    [SerializeField]
    GameObject secretRoomHalo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            secretRoom.SetActive(false);
            secretRoomHalo.SetActive(false);
            Player.Instance.secretHalo.SetActive(false);
        }
    }

}
