using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingState : PlayerState
{
    public LandingState(Player player) : base(player) => OnEnter();
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
