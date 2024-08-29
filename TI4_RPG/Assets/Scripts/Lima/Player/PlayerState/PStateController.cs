using UnityEngine;

public class PStateController : StateMachine
{
    [Space(10)]
    [Header("States")]
    public State Exploration, Combat, Cinematic;

    public CombatState combat;
    public Exploring exploring;

    [Space(10)] [Header("Components")] [SerializeField]
    private EngageSphere eDetection;
    
    
    protected override void Start()
    {
        base.Start();
        GameManager.Instance.enterCombat.AddListener(() => {
            EnterState(Combat);
            InputManager.Instance.SwitchCurrentActionMap("Combat");
        });
        GameManager.Instance.enterExploration.AddListener( () => {
            EnterState(Exploration);
            InputManager.Instance.SwitchCurrentActionMap("Action");
        });
        /*
        eDetection.OnDisengage.AddListener(() => {
            EnterState(Exploration);
            InputManager.Instance.SwitchCurrentActionMap("Action");
        });
        */
    }
    
}
