using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeRewindUI : MonoBehaviour
{
    [SerializeField]
    GameObject fade;
    [SerializeField]
    GameObject controls;
    [SerializeField]
    GameObject pauseButton;
    [SerializeField]
    Text text;
    [SerializeField]
    GameObject rewindButton;

    float timer;
    const int DEFAULT_LAYER = 0;

    void Start ()
    {
        timer = 7.99f;
        fade.SetActive(true);
        pauseButton.SetActive(false);
        controls.SetActive(false);
        if (TutorialUI.Instance.txt.text != "")
        {
            TutorialUI.Instance.oldmanFace.color -= new Color(0, 0, 0, TutorialUI.Instance.oldmanFace.color.a);
            TutorialUI.Instance.textBar.color -= new Color(0, 0, 0, TutorialUI.Instance.textBar.color.a);
            TutorialUI.Instance.txt.text = "";
        }
        
    }

	void Update ()
    {
        timer -= Time.deltaTime;
        text.text = timer.ToString()[0].ToString();
        if (timer <= 1)
        {
            UI.Instance.DeathUI.SetActive(true);
            this.gameObject.SetActive(false);
        }
	}

    public void RewindTime()
    {
        foreach (MeshRenderer mesh in Player.Instance.meshRenderer)
        {
            mesh.enabled = true;
        }

        fade.SetActive(false);
        pauseButton.SetActive(true);
        controls.SetActive(true);

        TimeController.isForward = false;
        TimeRecorder.isRecording = false;
        Player.Instance.isPlaying = true;
        TimeController.timeBufferStart = TimeController.internalTime;

        Player.Instance.gameObject.layer = DEFAULT_LAYER;
        Player.Instance.Health = 3;
        HealthUI.Instance.SetHealthbar();
        Player.Instance.AttackCollider.enabled = false;
        Player.Instance.transform.parent = null;
        Player.Instance.ChangeState(new PlayerIdleState());
        Player.Instance.ButtonMove(0);
        Player.Instance.myRigidbody.velocity = new Vector2(0, 0);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraEffect>().StartBlur(0.8f);
        Time.timeScale = 2;

        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        timer = 7.99f;
        fade.SetActive(true);
        pauseButton.SetActive(false);
        controls.SetActive(false);
    }

    public void Skip()
    {
        UI.Instance.DeathUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
