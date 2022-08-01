using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class LandedState : PlayerState
{
    int waitBeforeRunning = 500;
    bool canStartRunning;
    public LandedState(Player player) : base(player) => OnEnter();
    public async override void OnEnter()
    {
        player.SetAnimation(Player.AnimationStates.Landed);
        jumpCount = 0;
        await Task.Delay(waitBeforeRunning);
        canStartRunning = true;
    }

    public override void PhysicsProcess()
    {

    }

    public override PlayerState Process()
    {
        if (canStartRunning)
            return new RunningState(player);
        else
            return this;
    }
}
