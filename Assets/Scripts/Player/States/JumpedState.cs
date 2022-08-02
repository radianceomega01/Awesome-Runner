using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpedState : PlayerState
{
    float jumpPower = 300f;
    float doubleJumpPower = 200f;
    Collider[] colliders;

    public JumpedState(Player player) : base(player) => OnEnter();
    public override void OnEnter()
    {
        Debug.Log(jumpCount);
        if (jumpCount<=1)
        {
            jumpCount++;
            player.GetPlayerController().Player.Jump.performed += _ => player.SetState(new JumpedState(player));
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
    }

    public override PlayerState Process()
    {
        if (player.GetRigidBody().velocity.y < 0f)
            return new FallingState(player);
        else if (colliders != null)
        {
            if (player.GetRigidBody().velocity.y == 0f && colliders.Length > 0)
                return new LandedState(player);
            else
                return this;
        } 
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
        else
            return this;
    }
}
