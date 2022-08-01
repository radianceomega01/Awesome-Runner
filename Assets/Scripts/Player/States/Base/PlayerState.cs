using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    protected static int jumpCount;

    public PlayerState(Player player) 
    { 
        this.player = player;
    }
    public abstract void OnEnter();
    public abstract PlayerState Process();
    public abstract void PhysicsProcess();
}
