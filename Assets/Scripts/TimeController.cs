using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    const int TIME_BUFFER_SIZE = 800;

    public static int internalTime;

    public static bool isForward = true;

    public static int timeBufferStart;

    private void FixedUpdate()
    {
        //Debug.Log(internalTime);
        if (isForward && !Player.Instance.IsDead)
        {
            internalTime++;
        }
        else if (!isForward)
        {
            Player.Instance.immortal = true;
            internalTime--;
            internalTime = internalTime < 0 ? 0 : internalTime;
            if (internalTime <= timeBufferStart - 300 || internalTime == 0)
            {
                if (Player.Instance.OnGround || internalTime <= timeBufferStart - TIME_BUFFER_SIZE || internalTime == 0)
                {
                    StopRewindTime();
                }
            }
            
        }
    }

    void StopRewindTime()
    {
        StartCoroutine(Player.Instance.IndicateImmortal());

        TimeRecorder.states.Clear();
        internalTime = 0;

        isForward = true;
        TimeRecorder.isRecording = true;
        Player.Instance.isPlaying = false;

        Player.Instance.ChangeState(new PlayerIdleState());
        Player.Instance.myRigidbody.bodyType = RigidbodyType2D.Dynamic;
        Player.Instance.GetComponent<BoxCollider2D>().enabled = true;
        Player.Instance.GetComponent<CapsuleCollider2D>().enabled = true;
        Player.Instance.facingRight = Player.Instance.transform.localScale.x > 0;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraEffect>().StopBlur();
        Time.timeScale = 1;

        StartCoroutine(ImmortalTurnOff());
    }

    IEnumerator ImmortalTurnOff()
    {
        yield return new WaitForSeconds(Player.Instance.immortalTime);
        Player.Instance.immortal = false;
    }

    public void RewindTime()
    {
        TimeController.isForward = false;
        TimeRecorder.isRecording = false;
        Player.Instance.isPlaying = true;
        TimeController.timeBufferStart = internalTime;
    }
}
