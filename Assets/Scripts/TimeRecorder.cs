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

    [SerializeField]
    TimeController timeController;

    public static Dictionary<int, PlayerTimeState> states;

    public static bool isRecording = true;

	// Use this for initialization
	void Start ()
    {
        states = new Dictionary<int, PlayerTimeState>();
	}

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isRecording = false;
        }
        if (isRecording)
        {
            if (states.Count >= TIME_BUFFER_SIZE)//re-write states data
            {
                for (int i = timeController.time - TIME_BUFFER_SIZE; i <= timeController.time - TIME_BUFFER_SIZE; i++)
                {
                    states.Remove(i);
                }
            }
            states.Add(timeController.time, new PlayerTimeState(transform.position,
                                                                Player.Instance.currentState,
                                                                transform.localScale.x > 0));
        }
        else
        {
            Player.Instance.SetRecording(states);
        }
    }
}
