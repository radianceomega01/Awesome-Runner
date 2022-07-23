using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    PlayerController playerController;
    public PlayerState(Player player) 
    { 
        this.player = player;
        playerController = new PlayerController();
        playerController.Player.
    }
    public abstract void OnEnter();
    public abstract PlayerState Process();
    public abstract void PhysicsProcess();
}
