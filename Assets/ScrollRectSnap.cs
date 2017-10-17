using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour {

    public RectTransform panel;     //will hold the Scroll Panel
    public Button[] btn;
    public RectTransform center;    // Center to Compare the distance for each button


    private float[] distance;
    private bool dragging = false;   // will be true if we drag the panel
    private int btnDistance;         // will hold the distance between the buttons
    private int minButtonNum;



	// Use this for initialization
	void Start ()
    {
        int btnLength = btn.Length;
        distance = new float[btnLength];
        btnDistance = (int)Mathf.Abs(btn[1].GetComponent<RectTransform>().anchoredPosition.y - btn[0].GetComponent<RectTransform>().anchoredPosition.y);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    for (int i = 0; i < btn.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.y - btn[i].transform.position.y);
        }

        float minDistance = Mathf.Min(distance);

        for (int a = 0; a < btn.Length; a++)
        {
            if (minDistance == distance[a])
            {
                minButtonNum = a;
            }
        }

        if (!dragging)
        {
            LerpToBtn(minButtonNum * btnDistance);
        }



	}

    void LerpToBtn(int position)
    {
        float newY = Mathf.Lerp(panel.anchoredPosition.y, position, Time.deltaTime * 25f);
        Vector2 newPosition = new Vector2(panel.anchoredPosition.x, newY);


        panel.anchoredPosition = newPosition;
    }


    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;  
    }

}
