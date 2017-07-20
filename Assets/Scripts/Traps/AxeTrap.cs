using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrap : MonoBehaviour
{
    [SerializeField]
    GameObject axe;
    [SerializeField]
    float accelerationScale;
    [SerializeField]
    int startDirection = 1;
    int sign;
    int sign2 = -1;
    float acceleration = 1;
    Quaternion rotationVector;
    float before;
    float after;

    private void Start()
    {
        sign = startDirection;
    }

    void FixedUpdate ()
    {
        rotationVector = axe.transform.rotation;
        before = Mathf.Abs(rotationVector.z);
        acceleration -= accelerationScale * sign2;
        after = Mathf.Abs(rotationVector.z);
        rotationVector.z += 0.005f * sign * acceleration;
        axe.transform.rotation = rotationVector;
        if (Mathf.Abs(axe.transform.rotation.z) >= 0.73f)
        {
            sign *= -1;
            acceleration = 0.5f;
            rotationVector.z = axe.transform.rotation.z + 0.01f * sign;
            axe.transform.rotation = rotationVector;
        }
        sign2 = before > after ? 1 : -1;
	}
}
