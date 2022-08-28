using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class JumpedState : PlayerState
{
    float jumpPower = 400f;
    float doubleJumpPower = 300f;
    public static int jumpCount;
    Collider[] colliders;

    public JumpedState(Player player) : base(player) { }
    public override void OnEnter()
    {
        if (jumpCount<=1)
        {
            jumpCount++;
            //player.GetPlayerController().Player.Jump.performed += _ => player.SetState(StateFactory.GetJumpedState(player));
            JumpingBehaviour();
        }
    }

    void JumpingBehaviour()
    {
        if (jumpCount == 1)
        {
            player.GetRigidBody().AddForce(Vector3.up * jumpPower);
        }
        else if (jumpCount == 2)
        {
            player.GetRigidBody().AddForce(Vector3.up * doubleJumpPower);
        }
        player.SetAnimation(Player.AnimationStates.Jumped);
    }
    public override void PhysicsProcess()
    {
        colliders = Physics.OverlapSphere(player.GetFootTransform().position, 0.1f, player.GetGroundLayer());
        if (player.GetRigidBody().velocity.y < 0f)
        { 
            player.SetState(StateFactory.GetFallingState(player));
            return;
        }
        else if (player.GetRigidBody().velocity.y > 0f && colliders.Length >0)
            player.SetState(StateFactory.GetRunningState(player));
    }

    public override void Process()
    {
        if (player.GetPlayerController().Player.Jump.triggered)
            player.SetState(StateFactory.GetJumpedState(player));
    }

}
