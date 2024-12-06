using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Boss : State
{
    [Space(10)]
    [Header("Components")]
    [SerializeField] private Character self;
    [SerializeField] private Animator animator;
    [SerializeField] private Corujurso corujurso;

    private enum BEHAVIOUR
    {
        ATTACK,
        IDDLE
    }

    [Space(10)]
    [Header("Info")]
    [SerializeField] private BEHAVIOUR behaviour;
    [SerializeField] private SkillDataSO[] skills;
    [SerializeField] private SkillDataSO pulo;
    private SkillDataSO skillCasted;
    [SerializeField] private Character target;
    [SerializeField] private int animationID;
    [SerializeField] private float iddleTime = 3;
    private float _iddleTimer;
    private float timer;
    private bool jumping = false;


    // Adquire referencia da layer de anima??o e componentes
    private void Awake()
    {
        paused = true;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OnEnterState();
        paused = true;
    }

    private void OnDisable()
    {
        OnExitState();
    }

    public override State OnEnterState()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Character>();
        GameManager.Instance.CallCombatMode();
        return this;
    }

    public override void OnExitState()
    {
        target = null;
        StopAllCoroutines();
    }

    private void FixedUpdate()
    {
        if (paused) return;
        transform.LookAt(target.transform.position);
        switch (behaviour)
        {
            
            // Comportamento de ataque
            case BEHAVIOUR.ATTACK:
                if (Random.Range(0,100) > 80)
                {
                    corujurso.Jump();
                    jumping = true;
                    _iddleTimer = 1;
                    pulo.OnCast(self, target);
                    behaviour = BEHAVIOUR.IDDLE;
                }
                else
                {
                    skillCasted = skills[Random.Range(0,skills.Length)];
                    skillCasted.OnCast(self, target);
                    _iddleTimer = iddleTime + skillCasted.CastTime - Random.Range(0,3);
                    behaviour = BEHAVIOUR.IDDLE;
                    //animator.SetBool(animationPrimeryAttID, true);
                }

                break;

            case BEHAVIOUR.IDDLE:
                //animator.SetBool(animationPrimeryAttID, false);
                _iddleTimer -= Time.fixedDeltaTime;
                if (_iddleTimer <= 0)
                {
                    if (jumping)
                    {
                        corujurso.Fall();
                    }
                    behaviour = BEHAVIOUR.ATTACK;
                    jumping = false;                 
                }
                break;
        }
    }
}
