using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject boss;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    Transform bossCameraTransform;

    float bossCameraSize = 9;
    bool zoomOut = false;

    private void Update()
    {
        if (zoomOut && mainCamera.orthographicSize < 9)
        {
            mainCamera.orthographicSize += 0.01f;
        }
        else if (mainCamera.orthographicSize > 9)
        {
            enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.PlayMusic("boss drum", true);
            cameraTransform.position = bossCameraTransform.position;
            Player.Instance.bossFight = true;
            zoomOut = true;
            boss.SetActive(true);
        }
    }

}
