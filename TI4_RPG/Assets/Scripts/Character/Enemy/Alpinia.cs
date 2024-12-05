using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Alpinia : State
{
    [Space(10)]
    [Header("Components")]
    [SerializeField] private Character self;
    [SerializeField] private Animator animator;

    private enum BEHAVIOUR
    {
        ATTACK,
        IDDLE
    }

    [Space(10)]
    [Header("Info")]
    [SerializeField] private BEHAVIOUR behaviour;
    [SerializeField] private SkillDataSO primarySkill;
    [SerializeField] private Character target;
    [SerializeField] private int animationPrimeryAttID;
    [SerializeField] private float iddleTime = 3;
    private float _iddleTimer;
    private float timer;


    // Adquire referencia da layer de anima??o e componentes
    private void Awake()
    {
        paused = true;
        gameObject.SetActive(false);
        animationPrimeryAttID = Animator.StringToHash("IsAttack");
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
        switch (behaviour)
        {
            // Comportamento de ataque
            case BEHAVIOUR.ATTACK:

                primarySkill.OnCast(self, target);
                Debug.Log("Cast");
                _iddleTimer = iddleTime;
                behaviour = BEHAVIOUR.IDDLE;
                //animator.SetBool(animationPrimeryAttID, true);

                break;

            case BEHAVIOUR.IDDLE:
                //animator.SetBool(animationPrimeryAttID, false);
                _iddleTimer -= Time.fixedDeltaTime;
                if (_iddleTimer <= 0)
                {
                    behaviour = BEHAVIOUR.ATTACK;
                }
                break;
        }
    }
}
