using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KidHeadUI : MonoBehaviour
{
    private static KidHeadUI instance;
    public static KidHeadUI Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<KidHeadUI>();
            return instance;
        }
    }

    [SerializeField]
    public Sprite defaultHead;
    [SerializeField]
    public Sprite sadHead;
    [SerializeField]
    public Sprite angryHead;

    Image kidHead;

    private void Start()
    {
        kidHead = GetComponent<Image>();
    }

    public IEnumerator ShowEmotion(string emotion)
    {
        if (emotion == "angry")
        {
            kidHead.sprite = angryHead;
        }
        if (emotion == "sad")
        {
            kidHead.sprite = sadHead;
        }
        yield return new WaitForSeconds(0.5f);
        kidHead.sprite = defaultHead;
    }
}
