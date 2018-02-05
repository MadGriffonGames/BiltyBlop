using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    GameObject cameraObj;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Transform targetPoint;
    [SerializeField]
    float targetCameraSize;
    [SerializeField]
    GameObject lightning;
    [SerializeField]
    BossHolem holem;
    bool isActive = false;
    Vector3 plusVectorY = new Vector3(0, 0.04f, 0);
    Vector3 plusVectorX = new Vector3(0.02f, 0, 0);

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (isActive)
        {
            if (cam.transform.position.y <= targetPoint.position.y)
            {
                cam.transform.position += plusVectorY;
            }
            if (cam.transform.position.x <= targetPoint.position.x)
            {
                cam.transform.position += plusVectorX;
            }
            else
            {
                cam.transform.position -= plusVectorX;
            }

            if (cam.orthographicSize < targetCameraSize)
            {
                cam.orthographicSize += 0.04f;
            }
            else if(cam.orthographicSize >= targetCameraSize)
            {
                cam.orthographicSize = targetCameraSize;
                
            }

            if (cam.orthographicSize == targetCameraSize && isTargetPointReached())
            {
                StartCoroutine(ActivateHolem());
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            cam.gameObject.GetComponent<FollowCamera>().enabled = false;
            isActive = true;
        }
    }

    bool isTargetPointReached()
    {
        return Mathf.Abs(transform.position.y) - Mathf.Abs(transform.position.y) <= 0.05f;
    }

    IEnumerator ActivateHolem()
    {
        lightning.SetActive(true);
        SoundManager.PlaySound("lightning_sound1");
        SoundManager.PlaySound("holem_boss_appearance");
        yield return new WaitForSeconds(0.8f);
        holem.isActivated = true;
        this.gameObject.SetActive(false);
    }
}
