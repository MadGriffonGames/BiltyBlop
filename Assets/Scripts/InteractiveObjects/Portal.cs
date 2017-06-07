using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    GameObject nextPortal;
    [SerializeField]
    Animator MyAnimator;
    [SerializeField]
    GameObject PortalFadeImage;

    GameObject mainCamera;
    CanvasRenderer image;
    GameObject fade;
    bool showFade;
    bool hideFade;
    float colorA;

    const float opacity = 30;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        MyAnimator.enabled = false;
        image = PortalFade.Instance.fade.GetComponent<CanvasRenderer>();
        fade = PortalFade.Instance.fade;

        hideFade = false;
        showFade = false;
        colorA = 0;
        
    }

    private void Update()
    {
        SetPortalOpacity();
        //Debug.Log(image.GetAlpha());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            showFade = true;
            fade.SetActive(true);
        }
    }


    private void OnBecameVisible()
    {
        MyAnimator.enabled = true;
    }

    private void OnBecameInvisible()
    {
        MyAnimator.enabled = false;
    }

    private void SetPortalOpacity()
    {
        if (showFade)
        {
            
            image.SetAlpha(colorA);
            

            if (colorA < opacity)
                colorA += 2.5f;
            else
            {
                showFade = false;
                hideFade = true;
                mainCamera.transform.position = Vector3.Slerp(mainCamera.transform.position, nextPortal.transform.position + new Vector3(0, 0, -20), 1f);
                SoundManager.PlaySound("portal loud");
                Player.Instance.transform.position = nextPortal.transform.position + new Vector3(1 * Player.Instance.transform.localScale.x, -1.8f, -4);
            }
        }
        if (hideFade)
        {
            image.SetAlpha(colorA);

            if (colorA > 0)
                colorA -= 2.5f;
            else
            {
                hideFade = false;
                showFade = false;
                colorA = 0;
                fade.SetActive(false);
            }
        }
    }
}
