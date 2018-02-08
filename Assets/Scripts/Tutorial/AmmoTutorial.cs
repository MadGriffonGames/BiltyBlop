using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class AmmoTutorial : InAppTutorial
{
    [SerializeField]
    GameObject maneken;
    [SerializeField]
    GameObject tutorialTrigger;
    [SerializeField]
    GameObject bonusLight;

    bool isActivated;
    bool isCollected;

    private void Start()
    {
        isActivated = false;
        isCollected = false;
    }

    private void Update()
    {
        if (isCollected && (PlayerPrefs.GetInt ("Level2") == 0 || GameManager.developmentBuild))
        {
            Debug.Log(1);
            base.Update ();

			if (InventoryUI.isBonusBarOpen && !isActivated)
            {
				isActivated = true;
				light.SetActive (false);
				bonusLight.SetActive (true);
			}
		}
        else if (isCollected && PlayerPrefs.GetInt ("Level2") == 1) 
		{
			backpackLight.SetActive (false);
			DisactivateTutorial ();
		}
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        MakeThrowButtonBrighter();

        int currentCount = Inventory.Instance.GetItemCount (Inventory.AMMO);
        string currentLevel = SceneManager.GetActiveScene().name;
		isCollected = true;
		if (other.gameObject.CompareTag ("Player") && currentLevel == "Level1" && (PlayerPrefs.GetInt("Level2") == 0 || GameManager.developmentBuild)) 
		{
			Player.Instance.mobileInput = 0;
			inventoryFade.SetActive(true);
			EnableControls(false);
			currentCount = currentCount > 3 ? 3 : currentCount;
			Inventory.Instance.AddItem (Inventory.AMMO, 3 - currentCount);
			DevToDev.Analytics.Tutorial (2);
		}
        else 
		{
			Inventory.Instance.AddItem (Inventory.AMMO, 1);
			Inventory.Instance.UseAmmo ();
			DisactivateTutorial ();
			isActive = false;
		}
    }

    void MakeThrowButtonBrighter()
    {
        ControlsUI controls = UI.Instance.GetComponentInChildren<ControlsUI>();

        controls.throwButton.SetActive(true);
        Color tmp = controls.throwButtonImage.color;
        tmp.a = 1;
        controls.throwButtonImage.color = tmp;
    }
}
