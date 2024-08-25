using UnityEngine;

public class PStateController : StateMachine
{
    [Space(10)]
    [Header("States")]
    public State Exploration, Combat, Cinematic;

    [Space(10)] [Header("Components")] [SerializeField]
    private EngageSphere eDetection;
    
    
    protected override void Start()
    {
        base.Start();
        eDetection.OnEngage.AddListener(() => {
            EnterState(Combat);
            InputManager.Instance.SwitchCurrentActionMap("Combat");
        });
        Combat.GetComponent<CombatState>().OnEndCombat += () => {
            EnterState(Exploration);
            InputManager.Instance.SwitchCurrentActionMap("Action");
            Debug.Log("Exited combat");
        };
        /*
        eDetection.OnDisengage.AddListener(() => {
            EnterState(Exploration);
            InputManager.Instance.SwitchCurrentActionMap("Action");
        });
        */
    }
    
}
