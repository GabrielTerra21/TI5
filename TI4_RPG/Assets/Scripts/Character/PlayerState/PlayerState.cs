public abstract class PlayerState
{
    public Player agent;
    public PlayerState(Player agent) => this.agent = agent;
    public abstract void OnEnterState(); 
}
