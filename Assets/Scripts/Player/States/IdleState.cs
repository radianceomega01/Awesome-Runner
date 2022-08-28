
using System.Threading.Tasks;

public class IdleState : PlayerState
{
    int waitForStart = 1000;
    bool canStart;
    public IdleState(Player player) : base(player) { }

    public async override void OnEnter()
    {
        await Task.Delay(waitForStart);
        canStart = true;
    }

    public override void PhysicsProcess() 
    {
        if (canStart)
        { 
            player.SetState(StateFactory.GetRunningState(player));
            BGScroller.Instance.CanScroll = true;
        }
    }

    public override void Process() { }
}
