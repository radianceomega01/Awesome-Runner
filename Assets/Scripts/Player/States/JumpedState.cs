using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpedState : PlayerState
{
    float jumpPower = 400f;
    float doubleJumpPower = 300f;
    public static int jumpCount;
    Collider[] colliders;

    public JumpedState(Player player) : base(player) { }
    public override void OnEnter()
    {
        Debug.Log(GetType().Name);
        if (jumpCount<=1)
        {
            jumpCount++;
            player.GetPlayerController().Player.Jump.performed += _ => player.SetState(StateFactory.GetJumpedState(player));
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
    public override PlayerState PhysicsProcess()
    {
        colliders = Physics.OverlapSphere(player.GetFootTransform().position, 0.1f, player.GetGroundLayer());
        if (player.GetRigidBody().velocity.y < 0f)
            return StateFactory.GetFallingState(player);
        else if (colliders != null)
        {
            if (player.GetRigidBody().velocity.y == 0f && colliders.Length > 0)
                return StateFactory.GetRunningState(player);
            else
                return StateFactory.GetJumpedState(player);
        }
        else
            return StateFactory.GetJumpedState(player);
    }

    public override void Process()
    {
        /*if (colliders != null)
        {
            if (colliders.Length == 0 *//*&& player.GetRigidBody().velocity.y < 0f*//*)
            {
                Debug.Log(player.GetRigidBody().velocity.y);
                //return new FallingState(player);
            }
            *//*else if (colliders.Length > 0 && player.GetRigidBody().velocity.y == 0f)
                return new LandedState(player);*//*
            else
                return this;
        }*/
    }
}
