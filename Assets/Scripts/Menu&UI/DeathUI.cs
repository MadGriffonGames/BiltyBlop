using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    private static DeathUI instance;
    public static DeathUI Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<DeathUI>();
            return instance;
        }
    }
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
    [SerializeField]
    GameObject freeCheckpoints;
    [SerializeField]
    Sprite greenCircle;
    [SerializeField]
    Sprite redCircle;
    [SerializeField]
    GameObject buyCheckpointsUI;

    GameObject mainCamera;

    const int defaultLayer = 0;

    public void Start ()
    {
        SoundManager.PlayMusic("kid death", false);
        mainCamera = GameObject.FindWithTag("MainCamera");
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
        gameOverBar.GetComponent<Animator>().SetTrigger("disappear");

    }

    public void Continue()
    {
        SoundManager.PlayMusic("kid_music", true);
        if (Player.Instance.freeCheckpoints > 0)
        {
            SoundManager.PlayMusic("kid_music", true);
            gameOverBar.GetComponent<Animator>().SetTrigger("disappear");
            Player.Instance.freeCheckpoints--;

            Revive();

            foreach (GameObject enemy in GameManager.deadEnemies)
            {
                enemy.SetActive(true);
            }
            GameManager.deadEnemies.Clear();

            controls.SetActive(true);
            pauseButton.SetActive(true);
            fade.SetActive(false);
            this.gameObject.SetActive(false);
        }
        else
        {
            buyCheckpointsUI.SetActive(true);
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
            freeCheckpoints.GetComponentInChildren<Text>().text = Player.Instance.freeCheckpoints.ToString();
        }
    }

    void Revive()
    {
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
        Player.Instance.AttackCollider.enabled = false;
        Player.Instance.transform.parent = null;
        Player.Instance.ChangeState(new PlayerIdleState());
        Player.Instance.ButtonMove(0);
        Player.Instance.myRigidbody.velocity = new Vector2(0, 0);
    }

    private void OnEnable()
    {
        SoundManager.PlayMusic("kid death", false);

        Player.Instance.ResetBonusValues();

        pauseButton.SetActive(false);
        controls.SetActive(false);
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
        if (continueButton != null)
        {
            continueButton.SetActive(false);
            if (Player.Instance.freeCheckpoints > 0)
            {
                freeCheckpoints.GetComponent<Image>().sprite = greenCircle;
            }
            else
            {
                freeCheckpoints.GetComponent<Image>().sprite = redCircle;
            }
        }
        fade.SetActive(true);
        if (TutorialUI.Instance.txt.text != "")
        {
            TutorialUI.Instance.oldmanFace.color -= new Color(0, 0, 0, TutorialUI.Instance.oldmanFace.color.a);
            TutorialUI.Instance.textBar.color -= new Color(0, 0, 0, TutorialUI.Instance.textBar.color.a);
            TutorialUI.Instance.txt.text = "";
        }
        StartCoroutine(ButtonDelay());
    }

    public void UpdateFreeCheckpointsCounter()
    {
        freeCheckpoints.GetComponentInChildren<Text>().text = Player.Instance.freeCheckpoints.ToString();
        freeCheckpoints.GetComponent<Image>().sprite = greenCircle;
    } 
}
