using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(Player player):base(player) => OnEnter();

    public override void OnEnter()
    {
    }

    public override void PhysicsProcess()
    {
    }

    public override PlayerState Process()
    {
        return new RunningState(player);
    }
}
