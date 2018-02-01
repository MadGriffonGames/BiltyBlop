using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject fireball;
    [SerializeField]
    Transform[] instantiaitePoints;
    [SerializeField]
    public float throwDealy;
    [SerializeField]
    float speed;
    [SerializeField]
    float timeToLife;

    public IEnumerator SpawnFireballs(bool reverse)
    {
        if (!reverse)
        {
            for (int i = 0; i < instantiaitePoints.Length; i++)
            {
                ThrowFireball(i);
                yield return new WaitForSeconds(throwDealy);
            }
        }
        else
        {
            for (int i = instantiaitePoints.Length - 1; i >= 0; i--)
            {
                ThrowFireball(i);
                yield return new WaitForSeconds(throwDealy);
            }
        }
    }

    void ThrowFireball(int i)
    {
        GameObject tmp = (GameObject)Instantiate(fireball, instantiaitePoints[i].position, Quaternion.Euler(0, 0, 90));
        Vector3 vct = tmp.transform.localScale;
        vct *= 1.5f;
        tmp.transform.localScale = vct;
        tmp.GetComponent<VulkanFireball>().Initialize(Vector2.down, timeToLife, speed);
    }
}
