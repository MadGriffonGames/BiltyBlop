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

    public GameObject target;

    static float bossCameraSize =10.5f;
    bool zoomOut = false;

    private void Update()
    {
        if (zoomOut && mainCamera.orthographicSize < bossCameraSize)
        {
            mainCamera.orthographicSize += 0.02f;
        }
        else if (mainCamera.orthographicSize > bossCameraSize)
        {
            enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.PlayMusic("boss drum", true);
            Player.Instance.ChangeCameraTarget(bossCameraTransform.gameObject, new Vector3 (0,0,0));
            
            Player.Instance.bossFight = true;
            zoomOut = true;
            boss.SetActive(true);
        }
    }

}
