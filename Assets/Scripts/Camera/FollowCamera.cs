using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    float interpVelocityX, interpVelocityY;
    float minDistance;
    float followDistance;
    GameObject target;
    Vector3 targetPos;
    [SerializeField]
    float xMin, xMax;
    float lastX, lastY;
    Vector3 offsetY = new Vector3(0,0,0);
    Vector3 offset = new Vector3(0, 0, 0);

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("CameraTarget");
        targetPos = transform.position;
        lastX = target.transform.position.x;
        lastY = target.transform.position.y;
    }

    void CalculateOffsets()
    {
        float currentX = target.transform.position.x;
        float currentY = target.transform.position.y;

        if (currentX - lastX <= -0.11f)
            offset.x = -4f;
        else if (currentX - lastX >= 0.11f)
            offset.x = 4f;

        if (Mathf.Abs(Player.Instance.MyRigidbody.velocity.y) >= 14)
        {
            if (currentY - lastY < -0.01f)
                offset.y = -5f;
            else
                offset.y = 5f;
        }
        else
        {
            if (currentY - lastY < -0.01f)
                offset.y = -1f;
            else
                offset.y = 0;
        }

        lastX = currentX;
        lastY = currentY;

        offsetY.z = 0;
        offsetY = Vector3.Slerp(offsetY, new Vector3(offsetY.x, offset.y, offsetY.z), 0.1f);
    }

    void FixedUpdate()
    {
        this.CalculateOffsets();

        // Camera position with Target's position.z
        Vector3 posNoZ = transform.position; 
        posNoZ.z = target.transform.position.z;
        Vector3 targetDirection = (target.transform.position - posNoZ);

        // Adding Offsets
        targetDirection += offsetY;
        targetDirection.x += offset.x;

        interpVelocityY = targetDirection.magnitude * 10f;

        if (target.transform.position.x <= xMax && target.transform.position.x >= xMin)
        {
            interpVelocityX = targetDirection.magnitude * Mathf.Abs(Player.Instance.MyRigidbody.velocity.magnitude) * 5f;
        }
        else interpVelocityX = 0;

        targetPos = transform.position + (targetDirection.normalized * interpVelocityX * Time.deltaTime * Player.Instance.timeScaler);
        transform.position = Vector3.Slerp(transform.position, targetPos, 0.1f);
    }
}
