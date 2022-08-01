using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunningState : PlayerState
{
    float playerVelocity = 8f;
    bool startedFalling;

    public RunningState(Player player) : base(player) => OnEnter();
    public override void OnEnter()
    {
        player.SetAnimation(Player.AnimationStates.Running);
        player.GetPlayerController().Player.Jump.performed += _ => player.SetState(new JumpedState(player));
    }

    public override void PhysicsProcess()
    {
        player.GetRigidBody().velocity = new Vector3(playerVelocity, player.GetRigidBody().velocity.y, 0);
        if (Physics.OverlapSphere(player.GetFootTransform().position, 0.1f, player.GetGroundLayer()).Length == 0)
            startedFalling = true;
    }

    public override PlayerState Process()
    {
        if (startedFalling)
            return new FallingState(player);
        else
            return this;
    }
}
