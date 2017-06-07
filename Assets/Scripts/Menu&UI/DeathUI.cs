using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    GameObject pauseButton;
    [SerializeField]
    GameObject mainMenuButton;

    GameObject mainCamera;

    const int defaultLayer = 0;

    public void Start ()
    {
        SoundManager.PlayMusic("kid death", false);
        pauseButton.SetActive(false);
        controls.SetActive(false);
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
        if (continueButton != null)
        {
            continueButton.SetActive(false);
        }
        fade.SetActive(true);
        if (TutorialUI.Instance.txt.text != "")
        {
            TutorialUI.Instance.oldmanFace.color -= new Color(0, 0, 0, TutorialUI.Instance.oldmanFace.color.a);
            TutorialUI.Instance.textBar.color -= new Color(0, 0, 0, TutorialUI.Instance.textBar.color.a);
            TutorialUI.Instance.txt.text = "";
        }
        mainCamera = GameObject.FindWithTag("MainCamera");
        StartCoroutine(ButtonDelay());
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
            pauseButton.SetActive(false);
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
        SoundManager.PlayMusic("kid_music", true);
        if (GameManager.CollectedCoins >= 50)
        {
            SoundManager.PlayMusic("kid_music", true);
            gameOverBar.GetComponent<Animator>().SetBool("animate", false);

            GameManager.CollectedCoins -= 50;
            Player.Instance.gameObject.layer = defaultLayer; 
            Player.Instance.Health = 3;
            HealthUI.Instance.SetHealthbar();
            FindObjectOfType<Light>().intensity = Player.Instance.lightIntencityCP;
            Player.Instance.transform.position = new Vector3(Player.Instance.checkpointPosition.x,
                                                        Player.Instance.checkpointPosition.y,
                                                        Player.Instance.transform.position.z);

            mainCamera.transform.position = new Vector3(Player.Instance.transform.position.x,
                                                        Player.Instance.transform.position.y,
                                                        mainCamera.transform.position.z);
            
            Player.Instance.PlayerRevive();
            Player.Instance.ChangeState(new PlayerIdleState());
			Player.Instance.ButtonMove (0);
            Player.Instance.myRigidbody.velocity = new Vector2(0, 0);
            controls.SetActive(true);
            pauseButton.SetActive(true);
            fade.SetActive(false);
            this.gameObject.SetActive(false);
        }     
    }

    public void PlayUISound(string sound)
    {
        SoundManager.PlaySound(sound);
    }

    IEnumerator ButtonDelay()
    {
        yield return new WaitForSeconds(1);
        restartButton.SetActive(true);
        mainMenuButton.SetActive(true);
        if (continueButton != null)
        {
            continueButton.SetActive(true);
        }
    }

    private void OnEnable()
    {
        SoundManager.PlayMusic("kid death", false);
        Player.Instance.ResetBonusValues();
    }
}
