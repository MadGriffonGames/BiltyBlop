using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : InteractiveObject
{
    [SerializeField]
    GameObject lightPillar;
    [SerializeField]
    GameObject fire;
    [SerializeField]
    GameObject checkpointText;

    bool activated = false;

    public override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (!activated)
            {
                lightPillar.SetActive(true);
                fire.SetActive(true);
                checkpointText.SetActive(true);
                SoundManager.PlaySound("checkpoint");
                Player.Instance.checkpointPosition = this.gameObject.transform.position;
                Player.Instance.lightIntencityCP = FindObjectOfType<Light>().intensity;
                activated = true;
            }
        }
    }
}
