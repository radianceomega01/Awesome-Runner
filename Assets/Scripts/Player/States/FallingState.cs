using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState
{
    Collider[] colliders;
    public FallingState(Player player) : base(player) => OnEnter();
    public override void OnEnter()
     {
         player.GetPlayerController().Player.Jump.performed += _ => player.SetState(new JumpedState(player));
        player.SetAnimation(Player.AnimationStates.Falling);
    }

    public override void PhysicsProcess() 
    {
        colliders = Physics.OverlapSphere(player.GetFootTransform().position, 0.1f, player.GetGroundLayer());
    }

    public override PlayerState Process()
    {
        if (player.GetRigidBody().velocity.y == 0)
             return new LandedState(player);
        else
            return this;
    }
}
