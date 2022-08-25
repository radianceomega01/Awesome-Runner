using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState
{
    Collider[] colliders;
    public FallingState(Player player) : base(player) { }
    public override void OnEnter()
    {
         player.SetAnimation(Player.AnimationStates.Falling);
         player.GetPlayerController().Player.Jump.performed += _ => player.SetState(new JumpedState(player));
    }

    public override void PhysicsProcess() 
    {
        colliders = Physics.OverlapSphere(player.GetFootTransform().position, 0.1f, player.GetGroundLayer());
        //if (player.GetRigidBody().velocity.y == 0)
        if (colliders.Length > 0)
            player.SetState(StateFactory.GetRunningState(player));
    }

    public override void Process() { }
}
