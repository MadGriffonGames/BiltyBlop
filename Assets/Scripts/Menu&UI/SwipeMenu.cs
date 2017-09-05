using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{

    public RectTransform panel;
    public RectTransform center;
	public GameObject[] buttons;


    protected float[] distance;
    protected int buttonDistance;
    public int minButtonsNumber;
    protected bool dragging = false;
    protected bool onStart = true;
    protected bool tapping = false;

    public static int changingDistance = 3;

    public virtual void Start()
    {
        int buttonCount = buttons.Length;
        distance = new float[buttonCount];
        buttonDistance = (int)Mathf.Abs(buttons[1].GetComponent<RectTransform>().anchoredPosition.x - buttons[0].GetComponent<RectTransform>().anchoredPosition.x);
        panel.anchoredPosition = new Vector2(buttons[minButtonsNumber].transform.position.x, panel.anchoredPosition.y);
    }

    public virtual void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - buttons[i].transform.position.x);
        }

        float minDistance = Mathf.Min(distance);
        if (!tapping)
        {
            for (int i = 1; i < buttons.Length-1; i++)
            {
                if (minDistance == distance[i] && !onStart)
                {
                    minButtonsNumber = i;
                }
            }
        }

        if (!dragging || tapping)
        {
            LerpToButton(minButtonsNumber * -buttonDistance);
        }
    }
    public virtual void LerpToButton(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
        panel.anchoredPosition = newPosition;

        if (Mathf.Abs(panel.anchoredPosition.x - position) < changingDistance)
        {
            tapping = false;
        }
    }
    public virtual void OnButtonClickLerp(int buttonNumber)
    {
        if (buttonNumber == buttons.Length-1)
        {
            minButtonsNumber = buttons.Length - 2;
        }
		else if (buttonNumber == 0) {
			buttonNumber = 1;
		} 
		else
            minButtonsNumber = buttonNumber;
        tapping = true;
    }

    public void StartDrag()
    {
        dragging = true;
        onStart = false;
    }
    public void EndDrag()
    {
        dragging = false;
    }

    



}
