using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpedState : PlayerState
{
    public JumpedState(Player player) : base(player) => OnEnter();
    public override void OnEnter()
    {
    }

    public override void PhysicsProcess()
    {
    }

    public override PlayerState Process()
    {
        return null;
    }
}
