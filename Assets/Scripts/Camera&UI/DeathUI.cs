using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    [SerializeField]
    GameObject controls;

    Player player;

    void Start ()
    {
        controls.SetActive(false);
        player = FindObjectOfType<Player> ();
	}

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.collectedCoins = player.startCoinCount;
    }

    public void Continue()
    {
        if (GameManager.CollectedCoins >= 50)
        {
            GameManager.CollectedCoins -= 50;
            player.MyAniamtor.ResetTrigger("death");
            player.MyAniamtor.SetFloat("speed", 0);
            player.MyAniamtor.Play("PlayerIdle", 0);
            player.Health = 3;
            player.transform.position = player.CheckpointPosition;
            player.MyRigidbody.velocity = new Vector2(0, 0);
            controls.SetActive(true);
            this.gameObject.SetActive(false);
        }     
    }
}
