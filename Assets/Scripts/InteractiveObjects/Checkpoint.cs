using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : InteractiveObject
{
    [SerializeField]
    GameObject lightPillar;
    [SerializeField]
    GameObject light;
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
                light.SetActive(true);
                checkpointText.SetActive(true);
                SoundManager.PlaySound("checkpoint");
                Player.Instance.CheckpointPosition = this.gameObject.transform.position;
                activated = true;
            }
        }
    }
}
