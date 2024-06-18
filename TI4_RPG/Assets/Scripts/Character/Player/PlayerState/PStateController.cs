using UnityEngine;

public class PStateController : StateMachine
{
    [Space(10)]
    [Header("States")]
    [SerializeField] private State Exploration, Combat, Cinematic;
    
    [Space(10)]
    [Header("Components")]
    [SerializeField] private EngageSphere eDetection;
    
    
    protected override void Start()
    {
        base.Start();
        eDetection.OnEngage.AddListener(() => { EnterState(Combat); });
        eDetection.OnDisengage.AddListener(() => { EnterState(Exploration); });
    }
    
}
