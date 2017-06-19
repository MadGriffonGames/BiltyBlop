using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    const int TIME_BUFFER_SIZE = 800;

    public int time;

    public static bool isForward = true;

    public int timeBufferStart;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isForward = false;
            timeBufferStart = time;
        }
        if (isForward)
        {
            time++;
        }
        else
        {
            Player.Instance.immortal = true;
            time--;
            time = time < 0 ? 0 : time;
            if (time <= timeBufferStart - 300)
            {
                if (Player.Instance.OnGround || time <= timeBufferStart - TIME_BUFFER_SIZE || time == 0)
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
        time = 0;

        isForward = true;
        TimeRecorder.isRecording = true;
        Player.Instance.isPlaying = false;

        StartCoroutine(ImmortalTurnOff());
    }

    IEnumerator ImmortalTurnOff()
    {
        yield return new WaitForSeconds(Player.Instance.immortalTime);
        Player.Instance.immortal = false;
    }

    public void RewindTime()
    {
        isForward = false;
        TimeRecorder.isRecording = false;
        Player.Instance.isPlaying = true;
        timeBufferStart = time;
    }
}
