using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundInstantiator : MonoBehaviour {

    [SerializeField]
    GameObject currentGroundPref;

    [SerializeField]
    GameObject[] groundPrefs;

    GameObject mushroom;

    Vector3 currentGroundPosition;

    int mushroomFlag;

    int i;
    int lastResult;
    bool tmp = true;





    // Use this for initialization
    void Start () {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
        currentGroundPosition = currentGroundPref.transform.position;
        i = -1;
        mushroomFlag = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MiniSnowBall"))
        {
            for (int j = 0; j < 10; j++)
            {
                while (tmp)
                {
                    lastResult = i;
                    i = UnityEngine.Random.Range(0, 3);
                    if (i != lastResult)
                    {
                        tmp = false;
                    }
                }
                Instantiate(groundPrefs[i], currentGroundPosition + new Vector3(40f, 0), Quaternion.identity);
                mushroom = groundPrefs[i].transform.Find("Mushroom").gameObject;
                mushroomFlag = UnityEngine.Random.Range(0, 100);
                if (mushroomFlag > 90)
                {
                    mushroom.SetActive(false);
                }
                Debug.Log(mushroomFlag);
                tmp = true;
                this.gameObject.transform.position += new Vector3(40f, 0);
                currentGroundPosition += new Vector3(40f, 0);
                lastResult = i;
            }
        }
    }
}
