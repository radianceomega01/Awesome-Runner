using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class LandedState : PlayerState
{
    int waitBeforeRunning = 100;
    bool canStartRunning;
    public LandedState(Player player) : base(player) => OnEnter();
    public async override void OnEnter()
    {
        jumpCount = 0;
        player.SetAnimation(Player.AnimationStates.Landed);
        await Task.Delay(waitBeforeRunning);
        canStartRunning = true;
    }

    public override void PhysicsProcess() { }

    public override PlayerState Process()
    {
        if (canStartRunning)
            return new RunningState(player);
        else
            return this;
    }
}
