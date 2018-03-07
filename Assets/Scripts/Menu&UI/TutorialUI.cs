using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    private static TutorialUI instance;
    public static TutorialUI Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TutorialUI>();
            return instance;
        }
    }

    [SerializeField]
    public Text txt;
    [SerializeField]
    public Image oldmanFace;
    [SerializeField]
    public Image textBar;


	void Awake()
	{
		if (GameManager.screenAspect == GameManager.ASPECT_4x3)
		{
			Vector3 tmp = GetComponent<RectTransform> ().localPosition;
			tmp.y = 170;
			GetComponent<RectTransform> ().localPosition = tmp;
		}
	}
}
