using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraTest : MonoBehaviour
{
    float interpVelocityX, interpVelocityY;
    float minDistance;
    float followDistance;
    GameObject target;



    void Start()
    {

        target = GameObject.FindGameObjectWithTag("MiniGameCameraTarget");

        //targetPos = transform.position;
        //lastX = target.transform.position.x;
        //lastY = target.transform.position.y;

    }



    void LateUpdate()
    {
        transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);



        //Vector3 targetPos;
        //[SerializeField]
        //float xMin, xMax;
        //float lastX;
        ////Vector3 offsetY = new Vector3(0, 0, 0);
        //Vector3 offset = new Vector3(0, 0, 0);

        //const float fallingDeltaY = 0.4f;
        //const float runningDeltaX = 0.02f;
        //const int platformLayer = 9;


        //void FixedUpdate()
        //{
        //    CalculateOffsets();
        //    if (Player.Instance.bossFight)
        //        LerpToTargetWithoutOffsets();
        //    else
        //        LerpToTarget();
        //}

        //void CalculateOffsets()
        //{
        //    float currentX = target.transform.position.x;
        //    float currentY = target.transform.position.y;

        //    if (currentX - lastX <= -runningDeltaX)
        //        offset.x = -2f;
        //    else if (currentX - lastX >= runningDeltaX)
        //        offset.x = 2f;

        //    if (SnowballTest.Instance.GetComponent<Rigidbody2D>().velocity.x < 5)
        //    {
        //        offset.x = 0;
        //    }

        //    offset = Vector2.Lerp(offset, new Vector2(offset.x, 0), 0.65f);



        //    lastX = currentX;
        //}

        //public void LerpToTarget()
        //{
        //    // Camera position with Target's position.z
        //    Vector3 posNoZ = transform.position;
        //    posNoZ.z = target.transform.position.z;

        //    Vector3 targetDirection = (target.transform.position - posNoZ);
        //    // Adding Offsets 
        //    //targetDirection += offsetY;
        //    targetDirection.x += offset.x;

        //    if (target.transform.position.x <= xMax && target.transform.position.x >= xMin)
        //    {
        //        //if (SnowballTest.Instance.GetComponent<Rigidbody2D>().velocity.y <= -7f)
        //            //interpVelocityY = 150;
        //       // else if (SnowballTest.Instance.GetComponent<Rigidbody2D>().velocity.y <= 13f)
        //           // interpVelocityY = 20;
        //        //else interpVelocityY = 150;
        //        if (SnowballTest.Instance.GetComponent<Rigidbody2D>().velocity.x != 0)
        //            interpVelocityX = targetDirection.magnitude * Mathf.Abs(SnowballTest.Instance.GetComponent<Rigidbody2D>().velocity.x) * 3f;
        //        else interpVelocityX = targetDirection.magnitude * 12f;
        //    }
        //    else interpVelocityX = 0;
        //    if (SnowballTest.Instance.gameObject.layer == platformLayer)
        //    {
        //        interpVelocityX = 25;
        //    }
        //    targetPos = transform.position + new Vector3(targetDirection.x * interpVelocityX * Time.deltaTime, 0, 0);
        //    transform.position = Vector2.Lerp(transform.position, targetPos, 0.05f);
        //    transform.position = new Vector3(transform.position.x, transform.position.y, -20); // костыльный сет Z на позицмию камеры.
        //}

        //public void LerpToTargetWithoutOffsets()
        //{
        //    // Camera position with Target's position.z
        //    Vector3 posNoZ = transform.position;
        //    posNoZ.z = target.transform.position.z;

        //    Vector3 targetDirection = (target.transform.position - posNoZ);

        //    if (target.transform.position.x <= xMax && target.transform.position.x >= xMin)
        //    {
        //        //if (SnowballTest.Instance.GetComponent<Rigidbody2D>().velocity.y <= -7f)
        //            //interpVelocityY = 150;
        //        //else if (SnowballTest.Instance.GetComponent<Rigidbody2D>().velocity.y <= 13f)
        //            //interpVelocityY = 20;
        //        //else interpVelocityY = 150;
        //        if (SnowballTest.Instance.GetComponent<Rigidbody2D>().velocity.x != 0)
        //            interpVelocityX = targetDirection.magnitude * Mathf.Abs(SnowballTest.Instance.GetComponent<Rigidbody2D>().velocity.x) * 3f;
        //        else interpVelocityX = targetDirection.magnitude * 12f;
        //    }
        //    else interpVelocityX = 0;
        //    if (SnowballTest.Instance.gameObject.layer == platformLayer)
        //    {
        //        interpVelocityX = 25;
        //    }
        //    targetPos = transform.position + new Vector3(targetDirection.x * interpVelocityX * Time.deltaTime, 0, 0);
        //    transform.position = Vector2.Lerp(transform.position, targetPos, 0.05f);
        //    transform.position = new Vector3(transform.position.x, transform.position.y, -20); // костыльный сет Z на позицмию камеры.
        //}
    }
}