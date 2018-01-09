using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomInTrigger : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    GameObject target;
    [SerializeField]
    GameObject keyUI;
    [SerializeField]
    GameObject controllsUI;
    [SerializeField]
    GameObject healthUI;
    [SerializeField]
    GameObject moneyUI;
    [SerializeField]
    GameObject throwingUI;
    [SerializeField]
    GameObject inventoryUI;
    [SerializeField]
    GameObject starsUI;
    [SerializeField]
    GameObject pauseUI;
    [SerializeField]
    GameObject upPanel;
    [SerializeField]
    GameObject downPanel;

    static float cameraSize = 6f;
    bool zoomOut = false;
    bool fading;
    bool fadingOutUp = false;
    bool fadingOutDown = false;


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("CameraTarget");
    }

    private void Update()
    {
        if (zoomOut && mainCamera.orthographicSize > cameraSize)
        {
            mainCamera.orthographicSize -= 0.09f;
        }
        else if (mainCamera.orthographicSize < cameraSize)
        {
            enabled = false;
        }

        if (fadingOutUp)
        {
            FadeOutUp(upPanel);
        }
        if (fadingOutDown)
        {
            Debug.Log("Here");
            FadeOutDown(downPanel);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            fadingOutUp = true;
            fadingOutDown = true;
            //upPanel.SetActive(false);
            //downPanel.SetActive(false);
            EnableUIElements();
            Player.Instance.ChangeCameraTarget(target.gameObject, new Vector3(0, 0, 0));

            zoomOut = true;
        }
    }

    void EnableUIElements()
    {
        keyUI.SetActive(true);
        controllsUI.SetActive(true);
        healthUI.SetActive(true);
        moneyUI.SetActive(true);
        throwingUI.SetActive(true);
        inventoryUI.SetActive(true);
        starsUI.SetActive(true);
        pauseUI.SetActive(true);
    }


    void FadeOutUp(GameObject fadeObject)
    {
        fadeObject.gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, 0.1f);
        if (fadeObject.gameObject.GetComponent<Image>().color.a <= 0.2f)
        {
            fadeObject.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            fadeObject.gameObject.SetActive(false);
            fadingOutUp = false;
        }
    }

    void FadeOutDown(GameObject fadeObject)
    {
        fadeObject.gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, 0.1f);
        if (fadeObject.gameObject.GetComponent<Image>().color.a <= 0.2f)
        {
            fadeObject.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            fadeObject.gameObject.SetActive(false);
            fadingOutDown = false;
        }
    }

}