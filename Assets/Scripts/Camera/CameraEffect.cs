using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    private static Transform position;
    private static float elapsedShake, initiateDuration, initiatePower, percentComplete;
    private static Vector3 originalPosition;

    void Start()
    {
        percentComplete = 1;
        position = GetComponent<Transform>();
    }

    public static void Shake(float duration, float power)
    {
        if (percentComplete == 1) originalPosition = position.localPosition;
        elapsedShake = 0;
        initiateDuration = duration;
        initiatePower = power;
    }

    void Update()
    {
        if (elapsedShake < initiateDuration)
        {
            elapsedShake += Time.deltaTime;
            percentComplete = elapsedShake / initiateDuration;
            percentComplete = Mathf.Clamp01(percentComplete);
            Vector3 rnd = Random.insideUnitSphere * initiatePower * (1f - percentComplete);
            position.localPosition += new Vector3(rnd.x, rnd.y, 0);
        }
    }
}
