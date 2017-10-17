using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PlayerTimeState
{
    public Vector3 position;
    public IPlayerState animationState;
    public bool direction;

    public PlayerTimeState(Vector3 position, IPlayerState animationState, bool direction)
    {
        this.position = position;
        this.animationState = animationState;
        this.direction = direction;
    }
}

public class TimeRecorder : MonoBehaviour
{
    const int TIME_BUFFER_SIZE = 800;

    public static Dictionary<int, PlayerTimeState> states;

    public static bool isRecording = true;

	// Use this for initialization
	void Start ()
    {
        states = new Dictionary<int, PlayerTimeState>();
	}

    private void FixedUpdate()
    {
        if (isRecording && !Player.Instance.IsDead)
        {
            if (states.Count >= TIME_BUFFER_SIZE)//re-write states data
            {
                for (int i = TimeController.internalTime - TIME_BUFFER_SIZE; i <= TimeController.internalTime - TIME_BUFFER_SIZE; i++)
                {
                    states.Remove(i);
                }
            }
            states.Add(TimeController.internalTime, new PlayerTimeState(transform.position,
                                                                Player.Instance.currentState,
                                                                transform.localScale.x > 0));
        }
        else if (!isRecording)
        {
            Player.Instance.SetRecording(states);           
        }
    }
}
