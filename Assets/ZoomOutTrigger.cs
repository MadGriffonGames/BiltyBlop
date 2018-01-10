using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomOutTrigger : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    Transform bossCameraTransform;
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
    RectTransform upPanelTrans;
    RectTransform downPanelTrans;


    GameObject[] uiElements;

    static float cameraSize = 10.5f;
    bool zoomOut = false;
    bool isBlackPanels = false;

    private void Awake()
    {
        upPanelTrans = upPanel.GetComponent<RectTransform>();
        downPanelTrans = downPanel.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (zoomOut && mainCamera.orthographicSize < cameraSize)
        {
            mainCamera.orthographicSize += 0.2f;
        }
        else if (mainCamera.orthographicSize > cameraSize)
        {
            enabled = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            DisableUIElements();
            isBlackPanels = true;
            Player.Instance.ChangeCameraTarget(bossCameraTransform.gameObject, new Vector3(0, 0, 0));
            upPanel.SetActive(true);
            downPanel.SetActive(true);
            zoomOut = true;
        }
    }

    void DisableUIElements()
    {
        keyUI.SetActive(false);
        controllsUI.SetActive(false);
        healthUI.SetActive(false);
        moneyUI.SetActive(false);
        throwingUI.SetActive(false);
        inventoryUI.SetActive(false);
        starsUI.SetActive(false);
        pauseUI.SetActive(false);
    }

    
}