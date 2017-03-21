using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    float interpVelocity;
    float minDistance;
    float followDistance;
    GameObject target;
    Vector3 targetPos;
    [SerializeField]
    float xMin, xMax;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("CameraTarget");
        targetPos = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 posNoZ = transform.position;
        posNoZ.z = target.transform.position.z;
        Vector3 targetDirection = (target.transform.position - posNoZ); 
        if (target.transform.position.x <= xMax && target.transform.position.x >= xMin)
        {
            interpVelocity = targetDirection.magnitude * 40f;
        }
        else interpVelocity = 0;
        targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime * Player.Instance.timeScaler);
        transform.position = Vector3.Slerp(transform.position, targetPos, 0.2f);
    }
}
