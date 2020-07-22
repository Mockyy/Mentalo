using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum state { idle, moving, jumping, talking, pushing, holding }
    public state actualState;
    // Start is called before the first frame update
    void Start()
    {
        actualState = state.idle;
    }

    public state GetCurrentState()
    {
        return actualState;
    }

    public void SetState(state newState)
    {

        actualState = newState;
    }
}
