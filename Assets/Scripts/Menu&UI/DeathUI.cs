﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class DeathUI : MonoBehaviour
{
    [SerializeField]
    GameObject controls;
    [SerializeField]
    GameObject fade;

    private void Start ()
    {
        controls.SetActive(false);
        fade.SetActive(true);
        Advertisement.Show();
	}

    private void Update()
    {
        if (!fade.activeInHierarchy)
        {
            fade.SetActive(true);
        }
        if (controls.activeInHierarchy)
        {
            controls.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.collectedCoins = Player.Instance.startCoinCount;
    }

    public void Continue()
    {
        if (GameManager.CollectedCoins >= 50)
        {
            GameManager.CollectedCoins -= 50;
			Player.Instance.MyAniamtor.ResetTrigger ("death");      
            Player.Instance.MyAniamtor.Play("PlayerIdle", 0);
            Player.Instance.Health = 3;
			Player.Instance.MyAniamtor.SetTrigger ("revive");
            Player.Instance.transform.position = Player.Instance.CheckpointPosition;
			Player.Instance.MyAniamtor.SetFloat("speed", 0);
			Player.Instance.ButtonMove (0);
            Player.Instance.MyRigidbody.velocity = new Vector2(0, 0);
            controls.SetActive(true);
            fade.SetActive(false);
            this.gameObject.SetActive(false);
        }     
    }
}