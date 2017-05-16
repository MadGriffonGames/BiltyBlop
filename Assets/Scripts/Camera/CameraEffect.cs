using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    private static Transform position;
    private static float shakeElapsed, shakeDuration, initiatePower, percentComplete;
    private float bloodElapsed, bloodDuration;
    [SerializeField]
    GameObject blood;
    Color origColor;

    void Start()
    {
        percentComplete = 1;
        position = GetComponent<Transform>();
    }

    public static void Shake(float duration, float power)
    {
        shakeElapsed = 0;
        shakeDuration = duration;
        initiatePower = power;
    }

    public void StartBlur()
    {
        GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>().enabled = true;
    }

    public void StopBlur()
    {
        GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>().enabled = false;
    }

    public void ShowBlood(float duration)
    {
        blood.gameObject.SetActive(true);
        bloodElapsed = 0f;
        bloodDuration = duration;
    }

    void Update()
    {
  
        if (shakeElapsed < shakeDuration)
        {
            shakeElapsed += Time.deltaTime;
            percentComplete = shakeElapsed / shakeDuration;
            percentComplete = Mathf.Clamp01(percentComplete);
            Vector3 rnd = Random.insideUnitSphere * initiatePower * (1f - percentComplete);
            position.localPosition += new Vector3(rnd.x, rnd.y, 0);
        }
        if (bloodElapsed < bloodDuration)
        {
            bloodElapsed += Time.deltaTime;
            if (bloodDuration - bloodElapsed < 0.05f)
            {
                blood.gameObject.SetActive(false);
            }
        }
        if (percentComplete == 1 && Player.Instance.bossFight)
        {
            position.localPosition = new Vector3(0, 0, 0); 
        }
    }
}
