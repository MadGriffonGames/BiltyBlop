using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
		if (isCollected && (PlayerPrefs.GetInt ("Level2") == 0)) {
			base.Update ();

			if (InventoryUI.isBonusBarOpen && !isActivated) {
				isActivated = true;
				light.SetActive (false);
				bonusLight.SetActive (true);
			}
			//if (!InventoryUI.isOpen && bonusLight.activeInHierarchy)
			//{
			//    isActivated = false;
			//    isCollected = false;
			//}
		} else if (isCollected && PlayerPrefs.GetInt ("Level2") == 1) 
		{
			backpackLight.SetActive (false);
			DisactivateTutorial ();
		}
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

		int currentCount = Inventory.Instance.GetItemCount (Inventory.AMMO);
        string currentLevel = SceneManager.GetActiveScene().name;
		isCollected = true;
		if (other.gameObject.CompareTag ("Player") && currentLevel == "Level1" && PlayerPrefs.GetInt ("Level2") == 0) 
		{
			Player.Instance.mobileInput = 0;
			//inventoryFade.SetActive(true);
			EnableControls(false);
			currentCount = currentCount > 3 ? 3 : currentCount;
			Inventory.Instance.AddItem (Inventory.AMMO, 3 - currentCount);
			DevToDev.Analytics.Tutorial (2);
		} else 
		{
			Inventory.Instance.AddItem (Inventory.AMMO, 1);
			Inventory.Instance.UseAmmo ();
			DisactivateTutorial ();
			isActive = false;
		}
    }
}
