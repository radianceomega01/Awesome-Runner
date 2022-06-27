using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerState currentState;
    void Start()
    {
        currentState = new IdleState(this);
    }

    private void FixedUpdate()
    {
        currentState.PhysicsProcess();
    }
    void Update()
    {
        if(currentState != null)
            currentState = currentState.Process();
    }
}
