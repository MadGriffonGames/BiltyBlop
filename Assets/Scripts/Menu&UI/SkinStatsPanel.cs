using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinStatsPanel : MonoBehaviour {

    [SerializeField]
    GameObject[] defendIndicators;

    [SerializeField]
    GameObject[] attackIndicators;

    public void SetDefendIndicators(int stat)
    {
        if (stat > 0)
        {
            for (int i = 0; i < stat; i++)
            {
                defendIndicators[i].gameObject.SetActive(true);
            }
            if (stat < defendIndicators.Length)
            {
                for (int i = stat; i < defendIndicators.Length; i++)
                {
                    defendIndicators[i].gameObject.SetActive(false);
                }
            }
        }     
    }

    public void SetAttackIndicators(int stat)
    {
        if (stat > 0)
        {
            for (int i = 0; i < stat; i++)
            {
                attackIndicators[i].gameObject.SetActive(true);
            }
            if (stat < attackIndicators.Length)
            {
                for (int i = stat; i < attackIndicators.Length; i++)
                {
                    attackIndicators[i].gameObject.SetActive(false);
                }
            }
        }
    }

}
