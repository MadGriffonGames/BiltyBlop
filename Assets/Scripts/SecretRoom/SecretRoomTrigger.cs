using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject secretRoom;
    [SerializeField]
    GameObject secretRoomHalo;

    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.secretRoomer);
            //Debug.Log(PlayerPrefs.GetInt(AchievementManager.Instance.secretRoomer.achieveName));
            secretRoom.SetActive(false);
            secretRoomHalo.SetActive(false);
            Player.Instance.secretIndication.SetActive(false);
            Destroy(this);
        }
    }

}
