using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class AchievementUI : MonoBehaviour {

    [SerializeField]
    public GameObject achievementBox;
    bool timeToFade = false;
    bool fading = true;
    bool fadingOut = false;

	// Use this for initialization
	void Start () {
        achievementBox.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        fading = true;
    }

    private void Awake()
    {
        fading = true;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void FixedUpdate()
    {
        if (timeToFade)
        {
            FadeIn();
        }
        
        if (fadingOut)
        {
            FadeOut();
        }
    }

    public void AchievementAppear(string name)
    {
        achievementBox.gameObject.GetComponentInChildren<Text>().text = name;
        achievementBox.gameObject.SetActive(true);
        fading = true;
        timeToFade = true;
    }

    public IEnumerator AchievementDisappear()
    {
        yield return new WaitForSeconds(4);
        fadingOut = true;
        timeToFade = false;
    }

    void FadeIn()
    {
        if (fading)
        {
            achievementBox.gameObject.GetComponent<Image>().color += new Color(0, 0, 0, 0.05f);
            achievementBox.gameObject.GetComponentInChildren<Text>().color += new Color(0, 0, 0, 0.05f);
            if (achievementBox.gameObject.GetComponent<Image>().color.a >= 1f)
            {
                fading = false;
            }
        }
    }

    void FadeOut()
    {
        achievementBox.gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, 0.05f);
        achievementBox.gameObject.GetComponentInChildren<Text>().color -= new Color(0, 0, 0, 0.05f);
        if (achievementBox.gameObject.GetComponent<Image>().color.a <= 0.2f)
        {
            achievementBox.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            achievementBox.gameObject.SetActive(false);
            fadingOut = false;
            
        }
    }
}
