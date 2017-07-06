using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageFireball : MonoBehaviour
{
    [SerializeField]
    float speed;

    float cos;
    float acos;
    float angle;

    float scalar;
    float module;

    Vector2 myVector = new Vector2(1, 0);
    Vector2 targetVector;

    private void FixedUpdate()
    {
        targetVector = Player.Instance.transform.position - transform.position;
        scalar = targetVector.x * myVector.x + targetVector.y * myVector.y;
        module = Mathf.Sqrt(Mathf.Pow(targetVector.x, 2) + Mathf.Pow(targetVector.y, 2)) * Mathf.Sqrt(Mathf.Pow(myVector.x, 2) + Mathf.Pow(myVector.y, 2));
        cos = scalar / module;
        acos = Mathf.Acos(cos);
        float z = acos * Mathf.Rad2Deg * Mathf.Sign(targetVector.y - myVector.y);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
        transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, speed * Time.deltaTime);
    }
}
