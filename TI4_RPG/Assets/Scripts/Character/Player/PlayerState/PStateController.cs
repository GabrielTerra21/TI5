using UnityEngine;

public class PStateController : MonoBehaviour
{
    public PState Exploration, Combat, Cinematic, current;
    public EngageSphere eDetection;
    
    
    private void Start()
    {
        EnterState(Exploration);
        eDetection.OnEngage.AddListener(() => EnterState(Combat));
        eDetection.OnDisengage.AddListener(() => EnterState(Exploration));
    }

    public void EnterState(PState state)
    {
        current.OnExitState();
        current.enabled = false;
        state.enabled = true;
        current = state.OnEnterState();
    }
}
