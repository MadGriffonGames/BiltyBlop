using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    Image backpack;
    [SerializeField]
    Sprite backpackOpen;
    [SerializeField]
    Sprite backpackClose;
    [SerializeField]
    GameObject inventoryBar;
    [SerializeField]
    GameObject bonusBar;
    [SerializeField]
    GameObject inventoryFade;
    [SerializeField]
    Text hpCount;
    [SerializeField]
    Text clipsCount;
    [SerializeField]
    Text immortalCount;
    [SerializeField]
    Text damageCount;
    [SerializeField]
    Text speedCount;
    [SerializeField]
    Text timeCount;

    public static bool isOpen;
    public static bool isBonusBarOpen;

    private void Start()
    {
        isOpen = false;
        isBonusBarOpen = false;
        SetBoostersValues();
    }

    public void OpenInventory()
    {
        if (!isOpen)
        {
            ActivateInventory();
        }
        else
        {
            DisactivateInventory();
        }
    }

    void ActivateInventory()
    {
        Time.timeScale = 0;
        backpack.sprite = backpackOpen;
        isOpen = !isOpen;
        inventoryBar.SetActive(true);
        inventoryFade.SetActive(true);
        SetBoostersValues();
    }

    void DisactivateInventory()
    {
        Time.timeScale = Player.Instance.timeBonusNum > 0 ? 0.5f : 1;
        backpack.sprite = backpackClose;
        isOpen = !isOpen;
        bonusBar.SetActive(false);
        inventoryBar.SetActive(false);
        inventoryFade.SetActive(false);
        isBonusBarOpen = false;
    }

    public void HPbutton()
    {
        if (Inventory.Instance.GetItemCount(Inventory.HEAL) > 0 && Player.Instance.Health != Player.Instance.maxHealth)
        {
            Inventory.Instance.UseHP();
            hpCount.text = Inventory.Instance.GetItemCount(Inventory.HEAL).ToString();
            SoundManager.PlaySound("heart_collect");
            
            DisactivateInventory();
        }
    }

    public void BonusButton()
    {
        if (!bonusBar.activeInHierarchy)
        {
            bonusBar.SetActive(true);
            isBonusBarOpen = true;
        }
        else
        {
            bonusBar.SetActive(false);
            isBonusBarOpen = false;
        }
    }

    public void AmmoButton()
    {
        if (Inventory.Instance.GetItemCount(Inventory.AMMO) > 0 && Player.Instance.throwingIterator != Player.Instance.clipSize - 1)
        {
            SoundManager.PlaySound("ammo_inventory");
            Inventory.Instance.UseAmmo();
            clipsCount.text = Inventory.Instance.GetItemCount(Inventory.AMMO).ToString();

            DisactivateInventory();
        }
    }

    public void ImmortalButton()
    {
        if (Inventory.Instance.GetItemCount(Inventory.IMMORTAL_BONUS) > 0 && Player.Instance.immortalBonusNum == 0)
        {
            SoundManager.PlaySound("key_collect");
            Inventory.Instance.UseBonus(Inventory.IMMORTAL_BONUS);
            immortalCount.text = Inventory.Instance.GetItemCount(Inventory.IMMORTAL_BONUS).ToString();

            DisactivateInventory();
        }
        
    }

    public void DamageButton()
    {
        if (Inventory.Instance.GetItemCount(Inventory.DAMAGE_BONUS) > 0 && Player.Instance.damageBonusNum == 0)
        {
            SoundManager.PlaySound("key_collect");
            Inventory.Instance.UseBonus(Inventory.DAMAGE_BONUS);
            damageCount.text = Inventory.Instance.GetItemCount(Inventory.DAMAGE_BONUS).ToString();

            DisactivateInventory();
        }
    }

    public void SpeedButton()
    {
        if (Inventory.Instance.GetItemCount(Inventory.SPEED_BONUS) > 0 && Player.Instance.speedBonusNum == 0)
        {
            SoundManager.PlaySound("key_collect");
            Inventory.Instance.UseBonus(Inventory.SPEED_BONUS);
            speedCount.text = Inventory.Instance.GetItemCount(Inventory.SPEED_BONUS).ToString();

            DisactivateInventory();
        }
    }

    public void TimeButton()
    {
        if (Inventory.Instance.GetItemCount(Inventory.TIME_BONUS) > 0 && Player.Instance.timeBonusNum == 0)
        {
            SoundManager.PlaySound("key_collect");
            Inventory.Instance.UseBonus(Inventory.TIME_BONUS);
            timeCount.text = Inventory.Instance.GetItemCount(Inventory.TIME_BONUS).ToString();

            DisactivateInventory();
        }
    }

    void SetBoostersValues()
    {
        hpCount.text       = Inventory.Instance.GetItemCount(Inventory.HEAL).ToString();
        clipsCount.text    = Inventory.Instance.GetItemCount(Inventory.AMMO).ToString();
        immortalCount.text = Inventory.Instance.GetItemCount(Inventory.IMMORTAL_BONUS).ToString();
        damageCount.text   = Inventory.Instance.GetItemCount(Inventory.DAMAGE_BONUS).ToString();
        timeCount.text     = Inventory.Instance.GetItemCount(Inventory.TIME_BONUS).ToString();
        speedCount.text    = Inventory.Instance.GetItemCount(Inventory.SPEED_BONUS).ToString();
    }
}
