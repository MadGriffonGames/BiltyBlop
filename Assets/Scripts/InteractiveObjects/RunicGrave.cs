using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunicGrave : MonoBehaviour
{

    // Use this for initialization
    [SerializeField]
    GameObject particle;
    float maxY;
    float minY;
    float value = 0.027f;

    private void Start()
    {
        minY = this.transform.position.y - 0.7f;
        maxY = this.transform.position.y + 0.7f;

    }

    private void FixedUpdate()
    {
        MoveTowards();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Sword"))
        {
            if (!Player.Instance.bossFight)
            {
                CameraEffect.Shake(0.2f, 0.1f);
            }
            AchievementManager.Instance.CheckAchieve(AchievementManager.Instance.graver);
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            SoundManager.PlaySound("door_explode");
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (transform.position.y < -70)
        {
            Destroy(this.gameObject);
        }
    }

    void MoveTowards()
    {
        this.transform.position += new Vector3(0, value);
        if (this.transform.position.y >= maxY)
        {
            value *= -1;
        }
        if (this.transform.position.y <= minY)
        {
            value *= -1f;
        }
    }
}
