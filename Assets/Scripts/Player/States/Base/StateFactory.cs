
public static class StateFactory
{
    static PlayerState idleState;
    static PlayerState runningState;
    static PlayerState jumpedState;
    static PlayerState fallingState;

    public static PlayerState GetIdleState(Player player)
    {
        if (idleState == null)
            idleState = new IdleState(player);
        return idleState;
    }

    public static PlayerState GetRunningState(Player player)
    {
        if (runningState == null)
            runningState = new RunningState(player);
        return runningState;
    }

    public static PlayerState GetJumpedState(Player player)
    {
        if (jumpedState == null)
            jumpedState = new JumpedState(player);
        return jumpedState;
    }

    public static PlayerState GetFallingState(Player player)
    {
        if (fallingState == null)
            fallingState = new FallingState(player);
        return fallingState;
    }

    public static void ClearStates()
    {
        idleState = null;
        runningState = null;
        jumpedState = null;
        fallingState = null;
    }
}
