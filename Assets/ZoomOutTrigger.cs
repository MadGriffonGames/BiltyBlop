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
	GameObject leftArrow;
	[SerializeField]
	GameObject rightArrow;
	[SerializeField]
	GameObject jumpButton;
	[SerializeField]
	GameObject attackButton;
	[SerializeField]
	GameObject throwButton;
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
    Animator downPanelAnim;
    Animator upPanelAnim;


    GameObject[] uiElements;

    static float cameraSize = 10.5f;
    bool zoomOut = false;
    bool isBlackPanels = false;

    private void Awake()
    {
        upPanelTrans = upPanel.GetComponent<RectTransform>();
        downPanelTrans = downPanel.GetComponent<RectTransform>();
    }

    private void Start()
    {
        downPanelAnim = downPanel.GetComponent<Animator>();
        upPanelAnim = upPanel.GetComponent<Animator>();
    }

    private void Update()
    {
        if (zoomOut && mainCamera.orthographicSize < cameraSize)
        {
            mainCamera.orthographicSize += 0.1f;
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
            if (GameManager.screenAspect == GameManager.ASPECT_4x3)
            {
                upPanelAnim.Play("BlackPanelUPiPad");
                downPanelAnim.GetComponent<Animator>().Play("BlackPanelDowniPad");
            }
            DisableUIElements();
            isBlackPanels = true;
            Player.Instance.ChangeCameraTarget(bossCameraTransform.gameObject, new Vector3(0, 0, 0));
            upPanel.SetActive(true);
            downPanel.SetActive(true);
            zoomOut = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    void DisableUIElements()
    {
        keyUI.SetActive(false);

		leftArrow.GetComponent<Image> ().color = new Color (1, 1, 1, 0);
		rightArrow.GetComponent<Image> ().color = new Color (1, 1, 1, 0);
		attackButton.GetComponent<Image> ().color = new Color (1, 1, 1, 0);
		throwButton.GetComponent<Image> ().color = new Color (1, 1, 1, 0);
		jumpButton.GetComponent<Image> ().color = new Color (1, 1, 1, 0);

        healthUI.SetActive(false);
        moneyUI.SetActive(false);
        throwingUI.SetActive(false);
        inventoryUI.SetActive(false);
        starsUI.SetActive(false);
        pauseUI.SetActive(false);
    }

    
}