using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState
{
    Collider[] colliders;
    public FallingState(Player player) : base(player) => OnEnter();
    public override void OnEnter()
     {
         player.SetAnimation(Player.AnimationStates.Falling);
         player.GetPlayerController().Player.Jump.performed += _ => player.SetState(new JumpedState(player));
    }

    public override PlayerState PhysicsProcess() 
    {
        colliders = Physics.OverlapSphere(player.GetFootTransform().position, 0.1f, player.GetGroundLayer());
        //if (player.GetRigidBody().velocity.y == 0)
        if (colliders.Length > 0)
                return StateFactory.GetRunningState(player);
        else
            return StateFactory.GetFallingState(player);
    }

    public override void Process() { }
}
