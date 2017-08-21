using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RuneStone : InteractiveObject
{
    public string nextLvl;
	[SerializeField]
	private GameObject lightParticle;
    float delay = 2.5f;
    float timer;
    bool timerIsOn = false;

    public override void Start()
    {
        base.Start();
        enabled = true;
    }

    private void Update()
    {
        if (timerIsOn)
        {
            timer += Time.deltaTime;
        }
        if (timer >= delay)
        {
            Zoom.stopZoom();
            UI.Instance.LevelEndUI.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
        {
            SetLevelMetrics();

            SendMetrics();

            MyAnimator.SetTrigger("shine");
			StartCoroutine (WaitForGround ());
            Zoom.makeZoom(1, 3, 5);
            SaveGame();
            timer = 0;
            timerIsOn = true;
        }
    }

	public void MakeFX()
	{
		Instantiate(lightParticle, this.gameObject.transform.position + new Vector3(0, -0.5f, 1), Quaternion.Euler(new Vector3 (-90, 0 , 0)));
	}

    public void SaveGame()
    {
        GameManager.nextLevelName = nextLvl;
        PlayerPrefs.SetInt("Coins", GameManager.collectedCoins);
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_collects"))
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_collects") < Player.Instance.stars)
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_collects", Player.Instance.stars);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_collects", Player.Instance.stars);
        }

        if (PlayerPrefs.GetInt(nextLvl) == 0)
        {
            PlayerPrefs.SetString("LastUnlockedLevel", nextLvl);
            PlayerPrefs.SetInt(nextLvl, 1);
        }
        else
        {
            PlayerPrefs.SetInt(nextLvl, 1);
        }
    }

	IEnumerator WaitForGround()
	{
		yield return new WaitForSeconds (0.2f);
		Player.Instance.myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        Player.Instance.myRigidbody.freezeRotation = true;
        Player.Instance.ChangeState(new PlayerVictoryState());
    }

    void SetLevelMetrics()
    {
        MetricaManager.Instance.collectedStars = Player.Instance.stars;
        MetricaManager.Instance.collectedCoins = GameManager.lvlCollectedCoins;
    }

    void SendMetrics()
    {
        MetricaManager.Instance.SetParametrs();

        AppMetrica.Instance.ReportEvent(MetricaManager.Instance.currentLevel + " complete with:", MetricaManager.Instance.levelParams);
    }
}
