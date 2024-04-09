using UnityEngine;

public class PStateController : MonoBehaviour
{
    [Header("States")]
    [SerializeField] private PState Exploration, Combat, Cinematic, current;
    
    [Space(10)]
    [Header("Components")]
    [SerializeField] private EngageSphere eDetection;
    
    
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
