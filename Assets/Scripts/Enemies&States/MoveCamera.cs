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
    Vector3 plusVector = new Vector3(0, 0.04f, 0);
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (isActive)
        {
            if (cam.transform.position.y <= targetPoint.position.y)
            {
                cam.transform.position += plusVector;
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
                this.gameObject.SetActive(false);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            cam.gameObject.GetComponent<FollowCamera>().enabled = false;
            StartCoroutine(ActivateHolem());
            isActive = true;
        }
    }

    bool isTargetPointReached()
    {
        return Mathf.Abs(transform.position.y) - Mathf.Abs(transform.position.y) <= 0.1f;
    }

    IEnumerator ActivateHolem()
    {
        lightning.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        holem.isActivated = true;
    }
}
