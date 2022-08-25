
using System.Threading.Tasks;

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

    public override PlayerState PhysicsProcess() 
    {
        if (canStart)
            return StateFactory.GetRunningState(player);
        else
            return StateFactory.GetIdleState(player);
    }

    public override void Process() { }
}
