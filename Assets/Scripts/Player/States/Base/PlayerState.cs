
public abstract class PlayerState
{
    protected Player player;

    public PlayerState(Player player)
    {
        this.player = player;
    }
    public abstract void OnEnter();
    public abstract void Process();
    public abstract void PhysicsProcess();

}
