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
        colliders = Physics.OverlapSphere(player.GetFootTransform().position, 1f, player.GetGroundLayer());
    }

    public override PlayerState Process()
    {
        if (colliders != null)
            return new LandedState(player);
        else
            return this;
    }
}
