using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulkan : MonoBehaviour
{
    [SerializeField]
    GameObject fireball;

    [SerializeField]
    public float throwDealy;

    [SerializeField]
    Transform instantiaitePoint;

    [SerializeField]
    float speed;

    [SerializeField]
    float timeToLife;

    float timer;

    Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= throwDealy)
        {
            myAnimator.SetBool("Throw", true);
            timer = 0;
        }
    }

    public void ThrowFireballAnim()
    {
        ThrowFireball(timeToLife, speed);
    }

    public void ThrowFireball(float timeToLife, float speed)
    {
        if (this.gameObject.transform.rotation.eulerAngles.z == 90)
        {
            GameObject tmp = (GameObject)Instantiate(fireball, instantiaitePoint.position, Quaternion.identity);
            tmp.GetComponent<VulkanFireball>().Initialize(Vector2.left, timeToLife, speed);
        }

        if (this.gameObject.transform.rotation.eulerAngles.z == 270)
        {
            GameObject tmp = (GameObject)Instantiate(fireball, instantiaitePoint.position, Quaternion.Euler(0, 0, 180));
            tmp.GetComponent<VulkanFireball>().Initialize(Vector2.right, timeToLife, speed);
        }

        if (this.gameObject.transform.rotation.eulerAngles.z == 0)
        {
            GameObject tmp = (GameObject)Instantiate(fireball, instantiaitePoint.position, Quaternion.Euler(0, 0, 270));
            tmp.GetComponent<VulkanFireball>().Initialize(Vector2.up, timeToLife, speed);
        }

        if (Mathf.Abs(this.gameObject.transform.rotation.eulerAngles.z) == 180)
        {
            GameObject tmp = (GameObject)Instantiate(fireball, instantiaitePoint.position, Quaternion.Euler(0, 0, 90));
            tmp.GetComponent<VulkanFireball>().Initialize(Vector2.down, timeToLife, speed);
        }

        myAnimator.SetBool("Throw", false);
    }
}
