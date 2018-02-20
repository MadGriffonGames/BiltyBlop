using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromocodeWindow : MonoBehaviour {

    public InputField inputField;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject promoWindow;
    [SerializeField]
    Sprite acceptImage;
    [SerializeField]
    Sprite denyImage;
    [SerializeField]
    GameObject currentImg;
    [SerializeField]
    GameObject button;
    [SerializeField]
    GameObject giftWindow;
    [SerializeField]
    GameObject giftFade;
    [SerializeField]
    GameObject giantButton;

	// Use this for initialization
	void Start () {
        inputField.onValueChanged.AddListener(delegate { ToUpperCase(); });
        inputField.onValueChanged.AddListener(delegate { ButtonActivate(); });
        inputField.onValueChanged.AddListener(delegate { ButtonDeactivate(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ToUpperCase()
    {
        inputField.text = inputField.text.ToUpper();
    }

    public void OpenPromoWindow()
    {
        fade.SetActive(true);
        promoWindow.SetActive(true);
    }

    public void CheckPromoCode()
    {
        if (Promocodes.Instance.IsActivateCode(inputField.text))
        {
            currentImg.GetComponent<Image>().sprite = acceptImage;
            currentImg.GetComponent<Image>().color += new Color(0, 0, 0, 1);
            giftFade.SetActive(true);
            giftWindow.SetActive(true);
            giftWindow.GetComponentInChildren<Text>().text = Promocodes.Instance.promocodeGift;
            giantButton.SetActive(true);
        }
        else
        {
            currentImg.GetComponent<Image>().sprite = denyImage;
            currentImg.GetComponent<Image>().color += new Color(0, 0, 0, 1);
        }
    }

    public void ButtonActivate()
    {
        if (inputField.text.Length == 10)
        {
            button.GetComponent<Button>().interactable = true;
            button.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 1);
        }
    }

    public void ButtonDeactivate()
    {
        if (inputField.text.Length < 10)
        {
            button.GetComponent<Button>().interactable = false;
            button.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 0.2f);
        }
    }

    public void GiantButton()
    {
        giftFade.SetActive(false);
        giftWindow.SetActive(false);
        giantButton.SetActive(false);
        promoWindow.SetActive(false);
        fade.SetActive(false);
    }
}
