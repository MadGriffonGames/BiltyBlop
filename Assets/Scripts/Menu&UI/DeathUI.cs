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
	GameObject continueButton;
	[SerializeField]
	GameObject restartButton;

    GameObject mainCamera;

    public void Start ()
    {
        controls.SetActive(false);
        fade.SetActive(true);
        mainCamera = GameObject.FindWithTag("MainCamera");
		continueButton.SetActive (false);
		restartButton.SetActive (false);
        //Advertisement.Show();
	}

    private void Update()
    {
        if (this.isActiveAndEnabled)
        {
            gameOverBar.GetComponent<Animator>().SetBool("animate", true);
			this.StartCoroutine (EnableButtons());

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
			Player.Instance.MyAniamtor.ResetTrigger ("death");      
            Player.Instance.Health = 3;
			Player.Instance.MyAniamtor.SetTrigger ("revive");
            SoundManager.PlayMusic("kid death", false);
            Player.Instance.transform.position = new Vector3(Player.Instance.CheckpointPosition.x,
                                                        Player.Instance.CheckpointPosition.y,
                                                        Player.Instance.transform.position.z);
            Player.Instance.MyAniamtor.SetFloat("speed", 0);
			Player.Instance.ButtonMove (0);
            Player.Instance.MyRigidbody.velocity = new Vector2(0, 0);
            mainCamera.transform.position = new Vector3(Player.Instance.transform.position.x,
                                                        Player.Instance.transform.position.y,
                                                        mainCamera.transform.position.z);
            controls.SetActive(true);
            fade.SetActive(false);
            this.gameObject.SetActive(false);
        }     
    }

    private void OnEnable()
    {
        Player.Instance.ResetBonusValues();
    }

	private IEnumerator EnableButtons()
	{
		yield return new WaitForSeconds (2);
		continueButton.SetActive (true);
		restartButton.SetActive (true);
	}
}
