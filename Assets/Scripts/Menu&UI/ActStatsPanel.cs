using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActStatsPanel : MonoBehaviour
{
    [SerializeField]
    Text starsCount;
    [SerializeField]
    int actNumber;

    int count;

    void Start ()
    {
        count = 0;

        for (int i = 1 + 10 * (actNumber - 1); i <= 10 * actNumber; i++)
        {
            count += PlayerPrefs.GetInt("Level" + i + "_collects");
        }

        starsCount.text = count.ToString() + "\\30";
	}
}
