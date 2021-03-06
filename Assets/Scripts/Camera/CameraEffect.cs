﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraEffect : MonoBehaviour
{
    private static Transform position;
    private static float shakeElapsed, shakeDuration, initiatePower, percentComplete;
    private float bloodElapsed, bloodDuration;
    GameObject blood;
    Color origColor;
    bool hide = false;

    public static bool changeColors;
    PostProcessingProfile myProfile;
    float hueShift = 0;
    ColorGradingModel.Settings tmp;

    void Start()
    {
        percentComplete = 1;
        position = GetComponent<Transform>();
        blood = UI.Instance.transform.Find("Blood").gameObject;
        myProfile = GetComponent<PostProcessingBehaviour>().profile;
        myProfile.colorGrading.enabled = false;
        myProfile.chromaticAberration.enabled = false;
        myProfile.grain.enabled = false;
    }

    public static void Shake(float duration, float power)
    {
        shakeElapsed = 0;
        shakeDuration = duration;
        initiatePower = power;
    }

    public void StartBlur(float blurAmmount)
    {
        GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>().enabled = true;
        GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>().blurAmount = blurAmmount;
    }

    public void StopBlur()
    {
        GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>().enabled = false;
        GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>().blurAmount = 0.35f;
    }

    public void ShowBlood(float duration)
    {
        blood.GetComponent<Image>().color = new Color(blood.GetComponent<Image>().color.r,
                                                                  blood.GetComponent<Image>().color.g,
                                                                  blood.GetComponent<Image>().color.b,
                                                                  0.65f);
        blood.gameObject.SetActive(true);
        bloodElapsed = 0f;
        bloodDuration = duration;
    }

    void HideBlood()
    {
        blood.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.03f);
        if (blood.GetComponent<SpriteRenderer>().color.a <= 0)
        {
            blood.SetActive(false);
            hide = false;
        }
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
            if (bloodDuration - bloodElapsed < 0.1f)
            {
                    blood.SetActive(false);
            }
        }

        if (hide)
        {
            HideBlood();
        }

        if (percentComplete == 1 && Player.Instance.bossFight)
        {
            GetComponent<FollowCamera>().LerpToTargetWithoutOffsets();
        }

        if (changeColors)
        {
            myProfile.colorGrading.enabled = true;
            myProfile.chromaticAberration.enabled = true;
        }
    }

    public void ResetColors()
    {
        myProfile.colorGrading.enabled = false;
        myProfile.chromaticAberration.enabled = false;
    }

    public void SwitchOnRewindEffect(bool switchOn)
    {
        if (switchOn)
        {
            myProfile.chromaticAberration.enabled = true;
            myProfile.grain.enabled = true;
        }
        else
        {
            myProfile.chromaticAberration.enabled = false;
            myProfile.grain.enabled = false;
        }
    }
}
