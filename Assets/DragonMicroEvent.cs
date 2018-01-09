using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;


public class DragonMicroEvent : MonoBehaviour {

    [SerializeField]
    GameObject lightningBolt;
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

    private void Awake()
    {
        armature = changableTargetObject.GetComponent<UnityArmatureComponent>();

        dragonSlots = new Slot[8];
    }

    private void Start()
    {
        armature.animation.FadeIn("WEAKNESS_IDLE", -1, -1);
        SetSlots();
    }

    private void Update()
    {
        if (isOldDragon)
        {
            Debug.Log("Old Dragon");
            if (armature.animation.lastAnimationName == "WEAKNESS_IDLE")
            {
                armature.animation.FadeIn("WEAKNESS_END", -1, 1);
            }

            if (armature.animation.lastAnimationName == "WEAKNESS_END" && armature.animation.isCompleted)
            {
                Debug.Log("Starting to rise");
                armature.animation.timeScale = 1.5f;
                armature.animation.FadeIn("RISE", -1, 1);
            }

            if (armature.animation.lastAnimationName == "RISE" && armature.animation.isCompleted)
            {
                Debug.Log("Rised");
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
            StartCoroutine(ChangeDisplayIndexes());
            StartCoroutine(StopDisable());
        }
    }

    IEnumerator StopDisable()
    {
        yield return new WaitForSeconds(1.7f);
        stopCollider.SetActive(false);
    }

    IEnumerator ChangeDisplayIndexes()
    {
        yield return new WaitForSeconds(0.2f);
        ChangeIndexes();
        isOldDragon = true;
    }

    void AddSlot(string slotName, ref int i)
    {
        dragonSlots[i] = changableTargetObject.GetComponent<UnityArmatureComponent>().armature.GetSlot(slotName);
        i++;
    }

    private void SetSlots()
    {
        int i = 0;
        AddSlot("dragon_0007_", ref i);
        AddSlot("dragon_0005_", ref i);
        AddSlot("dragon_0006_", ref i);
        AddSlot("Body", ref i);
        AddSlot("dragon_0003", ref i);
        AddSlot("dragon_0002_", ref i);
        AddSlot("dragon_0001_", ref i);
        AddSlot("dragon_0000_", ref i);


        for (int j = 0; j < dragonSlots.Length; j++)
        {
            dragonSlots[j].displayIndex = 1;
        }

    }

    void ChangeIndexes()
    {
        for (int j = 0; j < dragonSlots.Length; j++)
        {
            dragonSlots[j].displayIndex = 0;
        }
    }

}
