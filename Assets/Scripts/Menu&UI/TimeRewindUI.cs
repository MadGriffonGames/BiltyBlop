using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeRewindUI : MonoBehaviour
{
    const int CRYSTAL_PRICE = 8;
    const int DEFAULT_LAYER = 0;

    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject controls;
    [SerializeField]
    GameObject pauseButton;
    [SerializeField]
    Text timerTxt;
    [SerializeField]
    GameObject rewindButton;
    [SerializeField]
    GameObject freeButton;
    [SerializeField]
    GameObject gameOverBar;
    float timer;
    bool isReviveFree = false;

    void Start ()
    {
        if (TutorialUI.Instance.txt.text != "")
        {
            TutorialUI.Instance.oldmanFace.color -= new Color(0, 0, 0, TutorialUI.Instance.oldmanFace.color.a);
            TutorialUI.Instance.textBar.color -= new Color(0, 0, 0, TutorialUI.Instance.textBar.color.a);
            TutorialUI.Instance.txt.text = "";
        }

        if (!PlayerPrefs.HasKey("RewindTimeTutorial") || PlayerPrefs.GetInt("FreeRevives") > 0)
        {
            PlayerPrefs.SetInt("RewindTimeTutorial", 1);
            isReviveFree = true;

            timerTxt.gameObject.SetActive(false);
            fade.SetActive(true);
            pauseButton.SetActive(false);
            controls.SetActive(false);
            rewindButton.SetActive(false);
            freeButton.SetActive(true);
        }
        else
        {
            fade.SetActive(true);
            pauseButton.SetActive(false);
            rewindButton.SetActive(true);
            controls.SetActive(false);            
        }
    
    }

	void Update ()
    {
            timer -= Time.deltaTime;
            timerTxt.text = timer.ToString()[0].ToString();
            if (timer <= 1)
            {
                UI.Instance.DeathUI.SetActive(true);
                this.gameObject.SetActive(false);
            }
	}

    public void BuyRewindTime()
    {
        if (PlayerPrefs.GetInt("FreeRevives") <= 0)
        {
            if (PlayerPrefs.GetInt("Crystals") >= CRYSTAL_PRICE)
            {
                PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") - CRYSTAL_PRICE);
                GameManager.crystalTxt.text = PlayerPrefs.GetInt("Crystals").ToString();

                RewindTime();
            }
            else
            {
                PurchaseManager.Instance.BuyConsumable(1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("FreeRevives", PlayerPrefs.GetInt("FreeRevives") - 1);
            RewindTime();
        }

    }

    public void RewindTime()
    {
        gameOverBar.SetActive(false);

        MetricaManager.Instance.rewindCount++;

        foreach (MeshRenderer mesh in Player.Instance.meshRenderer)
        {
            mesh.enabled = true;
        }

        fade.SetActive(false);
        pauseButton.SetActive(true);
        controls.SetActive(true);

        TimeController.isForward = false;
        TimeRecorder.isRecording = false;
        Player.Instance.isRewinding = true;
        TimeController.timeBufferStart = TimeController.internalTime;

        Player.Instance.gameObject.layer = DEFAULT_LAYER;
        Player.Instance.Health = (int)Player.Instance.maxHealth;
        HealthUI.Instance.SetHealthbar();
        Player.Instance.AttackCollider.enabled = false;
        Player.Instance.transform.parent = null;
        Player.Instance.ChangeState(new PlayerIdleState());
        Player.Instance.ButtonMove(0);
        Player.Instance.myRigidbody.velocity = new Vector2(0, 0);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraEffect>().StartBlur(0.8f);
        FindObjectOfType<Camera>().GetComponent<CameraEffect>().SwitchOnRewindEffect(true);
        Time.timeScale = 2;

        AppMetrica.Instance.ReportEvent("#REWIND_TIME Rewind time used in " + MetricaManager.Instance.currentLevel);
        DevToDev.Analytics.CustomEvent("#REWIND_TIME Rewind time used in " + MetricaManager.Instance.currentLevel);

        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        gameOverBar.SetActive(true);
        gameOverBar.GetComponent<Animator>().SetBool("animate", true);

        fade.SetActive(true);
        pauseButton.SetActive(false);
        controls.SetActive(false);
        timer = 7.99f;
        timerTxt.gameObject.SetActive(true);

        if (PlayerPrefs.HasKey("RewindTimeTutorial") && PlayerPrefs.GetInt("FreeRevives") <= 0)
        {
            isReviveFree = false;

            rewindButton.SetActive(true);
            freeButton.SetActive(false);
        }
    }

    public void Skip()
    {
        if (timer < 6)
        {
            UI.Instance.DeathUI.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
