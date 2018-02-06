using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [HideInInspector]
    public int speedBonusNum = 0;
    [HideInInspector]
    public int immortalBonusNum = 0;
    [HideInInspector]
    public int damageBonusNum = 0;
    [HideInInspector]
    public int jumpBonusNum = 0;
    [HideInInspector]
    public int timeBonusNum = 0;
    [HideInInspector]
    public float timeScaler = 1;
    [HideInInspector]
    public float timeScalerJump = 1;
    [HideInInspector]
    public float timeScalerMove = 1;
    [HideInInspector]
    public Animator bonusFX;
    [HideInInspector]
    public GameObject bonusFXObject;
    const float MOVEMENT_SPEED = 8;
    const int JUMP_FORCE = 700;


    private void Start()
    {
        bonusFXObject = Player.Instance.bonusFXObject;
        bonusFX = bonusFXObject.GetComponent<Animator>();
    }

    public void ExecBonusImmortal(float duration)
    {
        bonusFXObject.SetActive(true);
        StartCoroutine(ImmortalBonus(duration));
        MakeFX.Instance.MakeImmortalBonus(duration * Player.Instance.potionTimeScale);
        bonusFX.SetTrigger("immortal");
    }

    public IEnumerator ImmortalBonus(float duration)
    {
        immortalBonusNum++;
        Player.Instance.immortal = true;
        yield return new WaitForSeconds(duration * Player.Instance.potionTimeScale);
        immortalBonusNum--;
        if (immortalBonusNum == 0)
        {
            Player.Instance.immortal = false;
            bonusFX.SetTrigger("reset");
            bonusFXObject.SetActive(false);
        }
    }

    public void ExecBonusDamage(float duration)
    {
        StartCoroutine(DamageBonus(duration));
        MakeFX.Instance.MakeDamageBonus(duration * Player.Instance.potionTimeScale);
        bonusFX.SetTrigger("damage");

    }

    public IEnumerator DamageBonus(float duration)
    {
        damageBonusNum++;
        Player.Instance.meleeDamage *= 2;
        yield return new WaitForSeconds(duration * Player.Instance.potionTimeScale);
        damageBonusNum--;
        if (damageBonusNum == 0)
        {
            Player.Instance.meleeDamage /= 2;
            bonusFX.SetTrigger("reset");
            bonusFXObject.SetActive(false);
        }
    }

    public void ExecBonusJump(float duration)
    {
        bonusFXObject.SetActive(true);
        StartCoroutine(JumpBonus(duration));
        MakeFX.Instance.MakeJumpBonus(duration * Player.Instance.potionTimeScale);
        bonusFX.SetTrigger("jump");
    }

    public IEnumerator JumpBonus(float duration)
    {
        jumpBonusNum++;
        Player.Instance.jumpForce = 1200;
        yield return new WaitForSeconds(duration * Player.Instance.potionTimeScale);
        jumpBonusNum--;
        if (jumpBonusNum == 0)
        {
            Player.Instance.jumpForce = 700;
            bonusFX.SetTrigger("reset");
            bonusFXObject.SetActive(false);
        }
    }

    public void ExecBonusSpeed(float duration)
    {
        bonusFXObject.SetActive(true);
        StartCoroutine(SpeedBonus(duration));
        MakeFX.Instance.MakeSpeedBonus(duration * Player.Instance.potionTimeScale);
        bonusFX.SetTrigger("speed");
    }

    public IEnumerator SpeedBonus(float duration)
    {
        speedBonusNum++;
        Player.Instance.movementSpeed = 16;
        Player.Instance.myArmature.animation.timeScale = 2;
        timeScalerMove = 0.7f;
        Camera cam = Camera.main;
        CameraEffect cef = cam.GetComponent<CameraEffect>();
        cef.StartBlur(0.35f);
        yield return new WaitForSeconds(duration * Player.Instance.potionTimeScale);
        speedBonusNum--;

        if (speedBonusNum == 0)
        {
            Player.Instance.myRigidbody.gravityScale = 3;
            Player.Instance.movementSpeed = 8;
            Player.Instance.myArmature.animation.timeScale = 1;
            timeScalerMove = 1;
            cef.StopBlur();
            bonusFX.SetTrigger("reset");
            bonusFXObject.SetActive(false);
        }
    }

    public void ExecBonusTime(float duration)
    {
        bonusFXObject.SetActive(true);
        StartCoroutine(TimeBonus(duration));
        MakeFX.Instance.MakeTimeBonus(duration * Player.Instance.potionTimeScale);
        bonusFX.SetTrigger("time");
    }

    public IEnumerator TimeBonus(float duration)
    {
        timeBonusNum++;
        timeScaler = 1.6f;
        timeScalerJump = 3f;
        timeScalerMove = 1.8f;
        SoundManager.SetPitch(0.5f);
        Player.Instance.myArmature.animation.timeScale = 2f;
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.01f;
        Player.Instance.myRigidbody.gravityScale = 6;
        yield return new WaitForSeconds(duration * Player.Instance.potionTimeScale);
        timeBonusNum--;

        if (timeBonusNum == 0)
        {
            SoundManager.SetPitch(1f);
            timeScaler = 1;
            timeScalerJump = 1;
            timeScalerMove = 1;
            Player.Instance.myArmature.animation.timeScale = 1;
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
            Player.Instance.myRigidbody.gravityScale = 3;
            bonusFX.SetTrigger("reset");
            bonusFXObject.SetActive(false);
        }
    }

    public void ResetBonusValues()
    {
        SoundManager.SetPitch(1f);
        timeScaler = 1;
        timeScalerJump = 1;
        timeScalerMove = 1;
        Time.timeScale = 1;
        Player.Instance.myRigidbody.gravityScale = 3;
        Player.Instance.immortal = false;
        Player.Instance.meleeDamage = PlayerPrefs.GetInt("SwordAttackStat"); ;
        Player.Instance.movementSpeed = MOVEMENT_SPEED;
        Player.Instance.jumpForce = JUMP_FORCE;
        Player.Instance.myArmature.animation.timeScale = 1;
        Time.fixedDeltaTime = 0.02000000f;
        bonusFX.enabled = false;
    }

    public bool IsBonusUsed()
    {
        int tmp = damageBonusNum + immortalBonusNum + speedBonusNum + timeBonusNum;
        if (tmp > 0)
        {
            return true;
        }
        return false;
    }
}
