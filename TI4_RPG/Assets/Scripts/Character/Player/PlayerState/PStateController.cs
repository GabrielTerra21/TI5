using UnityEngine;

public class PStateController : MonoBehaviour
{
    public PState Exploration, Combat, Cinematic, current;
    
    public Player player;

    
    private void Start()
    {
        EnterState(Exploration);
    }

    public void EnterState(PState state)
    {
        state.enabled = true;
        current = state.OnEnterState();
    }
}
