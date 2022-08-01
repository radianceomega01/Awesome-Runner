using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class IdleState : PlayerState
{
    int waitForStart = 1000;
    bool canStart;
    public IdleState(Player player):base(player) => OnEnter();

    public async override void OnEnter()
    {
        await Task.Delay(waitForStart);
        canStart = true;
    }

    public override void PhysicsProcess()
    {
    }

    public override PlayerState Process()
    {
        if (canStart)
            return new RunningState(player);
        else
            return this;
    }
}
