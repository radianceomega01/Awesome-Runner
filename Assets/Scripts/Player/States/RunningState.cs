
using UnityEngine;

public class RunningState : PlayerState
{
    float playerVelocity = 8f;

    public RunningState(Player player) : base(player) { }
    public override void OnEnter()
    {
        Debug.Log(GetType().Name);
        player.SetAnimation(Player.AnimationStates.Running);
        player.GetPlayerController().Player.Jump.performed += _ => player.SetState(StateFactory.GetJumpedState(player));
        JumpedState.jumpCount = 0;
    }

    public override PlayerState PhysicsProcess()
    {
        player.GetRigidBody().velocity = new Vector3(playerVelocity, player.GetRigidBody().velocity.y, 0);
        /*if (Physics.OverlapSphere(player.GetFootTransform().position, 0.1f, player.GetGroundLayer()).Length == 0)
        //if (player.GetRigidBody().velocity.y < -2)
            return StateFactory.GetFallingState(player);
        else*/
            return StateFactory.GetRunningState(player);

    }

    public override void Process() { }

}
