using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Advertisements;

public class DeathUI : MonoBehaviour
{
    [SerializeField]
    GameObject controls;
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject gameOverBar;
    [SerializeField]
    GameObject restartButton;
    [SerializeField]
    GameObject continueButton;

    GameObject mainCamera;

    public void Start ()
    {
        SoundManager.PlaySound("kid death");
        controls.SetActive(false);
        restartButton.SetActive(false);
        continueButton.SetActive(false);
        fade.SetActive(true);
        mainCamera = GameObject.FindWithTag("MainCamera");
        StartCoroutine(ButtonDelay());
        //Advertisement.Show();
	}

    private void Update()
    {
        if (this.isActiveAndEnabled)
        {
            gameOverBar.GetComponent<Animator>().SetBool("animate", true);
        }
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
        gameOverBar.GetComponent<Animator>().SetBool("animate", false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.collectedCoins = Player.Instance.startCoinCount;
    }

    public void Continue()
    {
        if (GameManager.CollectedCoins >= 50)
        {
            gameOverBar.GetComponent<Animator>().SetBool("animate", false);

            GameManager.CollectedCoins -= 50;
            Player.Instance.gameObject.layer = 0;
			Player.Instance.MyAniamtor.ResetTrigger ("death");      
            Player.Instance.Health = 3;
			Player.Instance.MyAniamtor.SetTrigger ("revive");
            FindObjectOfType<Light>().intensity = Player.Instance.lightIntencityCP;
            Player.Instance.transform.position = new Vector3(Player.Instance.checkpointPosition.x,
                                                        Player.Instance.checkpointPosition.y,
                                                        Player.Instance.transform.position.z);

            mainCamera.transform.position = new Vector3(Player.Instance.transform.position.x,
                                                        Player.Instance.transform.position.y,
                                                        mainCamera.transform.position.z);

            Player.Instance.MyAniamtor.SetFloat("speed", 0);
			Player.Instance.ButtonMove (0);
            Player.Instance.myRigidbody.velocity = new Vector2(0, 0);
            controls.SetActive(true);
            fade.SetActive(false);
            this.gameObject.SetActive(false);
        }     
    }

    IEnumerator ButtonDelay()
    {
        yield return new WaitForSeconds(1);
        restartButton.SetActive(true);
        continueButton.SetActive(true);
    }

    private void OnEnable()
    {
        Player.Instance.ResetBonusValues();
    }
}
