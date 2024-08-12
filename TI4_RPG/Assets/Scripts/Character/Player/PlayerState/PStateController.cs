using UnityEngine;
using UnityEngine.InputSystem;

public class PStateController : StateMachine
{
    [Space(10)]
    [Header("States")]
    [SerializeField] private State Exploration, Combat, Cinematic;
    
    [Space(10)]
    [Header("Components")]
    [SerializeField] private EngageSphere eDetection;
    [SerializeField] private PlayerInput playerInput;
    
    
    protected override void Start()
    {
        base.Start();
        eDetection.OnEngage.AddListener(() => {
            EnterState(Combat);
            playerInput.SwitchCurrentActionMap("Combat");
        });
        eDetection.OnDisengage.AddListener(() => {
            EnterState(Exploration);
            playerInput.SwitchCurrentActionMap("Action");
        });
    }
    
}
