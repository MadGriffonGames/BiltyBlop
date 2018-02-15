using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;


public class DragonMicroEvent : MonoBehaviour {

    [SerializeField]
    GameObject lightningBolt;
    [SerializeField]
    GameObject lighningBolt1;
    [SerializeField]
    GameObject lightningBolt2;
    [SerializeField]
    GameObject changableTargetObject;
    [SerializeField]
    GameObject stopCollider;
    Slot[] dragonSlots;
    int dragonIndex;
    bool isOldDragon = false;
    UnityArmatureComponent armature;
    bool isUp = false;
    bool isStop = true;
    List<Slot> slots;

    private void Awake()
    {
        armature = changableTargetObject.GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(Player.Instance.GetComponent<CapsuleCollider2D>(), this.GetComponent<Collider2D>(), true);
    }

    private void Start()
    {
        slots = armature.armature.GetSlots();
        armature.animation.timeScale = 1.3f;
        armature.animation.FadeIn("WEAKNESS_IDLE", -1, -1);
    }

    private void Update()
    {
        if (isOldDragon)
        {
            if (armature.animation.lastAnimationName == "WEAKNESS_IDLE")
            {
                armature.animation.FadeIn("WEAKNESS_END", -1, 1);
                changableTargetObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * transform.localPosition.x / 73, 1.5f * transform.localPosition.y);

            }

            if (armature.animation.lastAnimationName == "WEAKNESS_END" && armature.animation.isCompleted)
            {
                //armature.animation.timeScale = 1.5f;
                armature.animation.FadeIn("RISE", -1, 1);
                changableTargetObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-2 * transform.localPosition.x / 73, 1.3f * transform.localPosition.y / 2);
            }

            if (armature.animation.lastAnimationName == "RISE" && armature.animation.isCompleted)
            {
                isUp = true;
                armature.animation.FadeIn("FLY", -1, -1);
            }
            
            if (isUp)
            {
                changableTargetObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-2 * transform.localPosition.x/70, -1.3f * transform.localPosition.y/10);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightningBolt.SetActive(true);
            SoundManager.PlaySound("lightning_sound1");
            StartCoroutine(ThrowSecondLight());
            StartCoroutine(ThrowTrhirdLight());
            StartCoroutine(ChangeDisplayIndexes());
            StartCoroutine(StopDisable());
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator StopDisable()
    {
        yield return new WaitForSeconds(2.7f);
        stopCollider.SetActive(false);
    }

    IEnumerator ChangeDisplayIndexes()
    {
        foreach (Slot slot in slots)
        {
            slot.displayController = "none";
        }
        yield return new WaitForSeconds(0.2f);
        ChangeIndexes();
        isOldDragon = true;
    }

    void AddSlot(Slot slot, ref int i)
    {
        dragonSlots[i] = slot;
        i++;
    }

    //private void SetSlots()
    //{

    //    int i = 0;
    //    AddSlot(dragon7, ref i);
    //    AddSlot(dragon5, ref i);
    //    AddSlot(dragon6, ref i);
    //    AddSlot(body, ref i);
    //    AddSlot(dragon3, ref i);
    //    AddSlot(dragon2, ref i);
    //    AddSlot(dragon1, ref i);
    //    AddSlot(dragon0, ref i);


    //    for (int j = 0; j < dragonSlots.Length; j++)
    //    {
    //        dragonSlots[j].displayIndex = 1;
    //    }

    //}

    void ChangeIndexes()
    {
        foreach (Slot slot in slots)
        {
            slot.displayIndex = 0;
        }
    }

    IEnumerator ThrowSecondLight()
    {
        yield return new WaitForSeconds(0.2f);
        lighningBolt1.SetActive(true);
        SoundManager.PlaySound("lightning_sound1");
    }

    IEnumerator ThrowTrhirdLight()
    {
        yield return new WaitForSeconds(0.4f);
        lightningBolt2.SetActive(true);
        SoundManager.PlaySound("lightning_sound1");
    }

}
