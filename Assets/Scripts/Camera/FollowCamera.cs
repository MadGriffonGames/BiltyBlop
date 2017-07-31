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

    const float fallingDeltaY = 0.4f;
    const float runningDeltaX = 0.02f;
    const int platformLayer = 9;

    void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("CameraTarget");
        
        targetPos = transform.position;
        lastX = target.transform.position.x;
        lastY = target.transform.position.y;

    }

    void FixedUpdate()
    {
        CalculateOffsets();
        if (Player.Instance.bossFight)
            LerpToTargetWithoutOffsets();
        else
            LerpToTarget();
    }

    void CalculateOffsets()
    {
        float currentX = target.transform.position.x;
        float currentY = target.transform.position.y;

        if (currentX - lastX <= -runningDeltaX)
            offset.x = -4f;
        else if (currentX - lastX >= runningDeltaX)
            offset.x = 4f;

        if (Mathf.Abs(currentY - lastY) >= fallingDeltaY)
        {
            offset.y = 5*Mathf.Sign(currentY - lastY);
        }
        else
        {
            if (currentY - lastY < -0.05f)
                offset.y = -3;
            else if (currentY - lastY < -0.15f)
                    offset.y = -2.5f;
                else
                    offset.y = 0;
        }
        //Debug.Log(offset.y + "    " + (currentY-lastY));
        lastX = currentX;
        lastY = currentY;

        offsetY.z = 0;
        offsetY.x = 0;
        offsetY = Vector2.Lerp(offsetY, new Vector2(0, offset.y), 0.1f);
        //offsetY = Vector3.Slerp(offsetY, new Vector3(offsetY.x, offset.y, offsetY.z), 0.1f);
    }

    public void LerpToTarget()
    {
        // Camera position with Target's position.z
        Vector3 posNoZ = transform.position;
        posNoZ.z = target.transform.position.z;

        Vector3 targetDirection = (target.transform.position - posNoZ);
        // Adding Offsets 
        targetDirection += offsetY;
        targetDirection.x += offset.x;

        if (target.transform.position.x <= xMax && target.transform.position.x >= xMin)
        {
            if (Player.Instance.myRigidbody.velocity.y <= -7f)
                interpVelocityY = 150;
            else if (Player.Instance.myRigidbody.velocity.y <= 13f)
                interpVelocityY = 20;
            else interpVelocityY = 150;
            if (Player.Instance.myRigidbody.velocity.x != 0)
                interpVelocityX = targetDirection.magnitude * Mathf.Abs(Player.Instance.myRigidbody.velocity.x) * 3f;
            else interpVelocityX = targetDirection.magnitude * 12f;
        }
        else interpVelocityX = 0;
        if (Player.Instance.gameObject.layer == platformLayer)
        {
            interpVelocityX = 25;
        }
        targetPos = transform.position + new Vector3(targetDirection.x * interpVelocityX * Time.deltaTime * Player.Instance.timeScaler, targetDirection.y * interpVelocityY * Time.deltaTime * Player.Instance.timeScaler, 0);
        transform.position = Vector2.Lerp(transform.position, targetPos, 0.05f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -20); // костыльный сет Z на позицмию камеры.
    }

    public void LerpToTargetWithoutOffsets()
    {
        // Camera position with Target's position.z
        Vector3 posNoZ = transform.position;
        posNoZ.z = target.transform.position.z;

        Vector3 targetDirection = (target.transform.position - posNoZ);

        if (target.transform.position.x <= xMax && target.transform.position.x >= xMin)
        {
            if (Player.Instance.myRigidbody.velocity.y <= -7f)
                interpVelocityY = 150;
            else if (Player.Instance.myRigidbody.velocity.y <= 13f)
                interpVelocityY = 20;
            else interpVelocityY = 150;
            if (Player.Instance.myRigidbody.velocity.x != 0)
                interpVelocityX = targetDirection.magnitude * Mathf.Abs(Player.Instance.myRigidbody.velocity.x) * 3f;
            else interpVelocityX = targetDirection.magnitude * 12f;
        }
        else interpVelocityX = 0;
        if (Player.Instance.gameObject.layer == platformLayer)
        {
            interpVelocityX = 25;
        }
        targetPos = transform.position + new Vector3(targetDirection.x * interpVelocityX * Time.deltaTime * Player.Instance.timeScaler, targetDirection.y * interpVelocityY * Time.deltaTime * Player.Instance.timeScaler, 0);
        transform.position = Vector2.Lerp(transform.position, targetPos, 0.05f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -20); // костыльный сет Z на позицмию камеры.
    }
}
