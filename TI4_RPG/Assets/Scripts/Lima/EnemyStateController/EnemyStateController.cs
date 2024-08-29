using UnityEngine;

public class EnemyStateController : StateMachine {
     [Space(10)]
     [Header("States")]
     [SerializeField] public State Iddle, Combat, Fleeing;
    
     [Space(10)]
     [Header("Components")]
     [SerializeField] private EngageSphere eDetection;
    
    
     protected override void Start()
     {
          base.Start();
          eDetection.OnEngage.AddListener(() => EnterState(Combat));
     }
}
