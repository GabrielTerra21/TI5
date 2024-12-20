using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    private SkillDataSO skillCasted;
    [SerializeField] private Character target;
    [SerializeField] private int animationID;
    [SerializeField] private float iddleTime = 3;
    private float _iddleTimer;
    private float timer;


    // Adquire referencia da layer de anima??o e componentes
    private void Awake()
    {
        _iddleTimer = 2f;
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
        
        switch (behaviour)
        {
            
            // Comportamento de ataque
            case BEHAVIOUR.ATTACK:
                transform.LookAt(target.transform.position);
   
                    skillCasted = skills[Random.Range(0,skills.Length)];
                    skillCasted.OnCast(self, target);
                    _iddleTimer = iddleTime + skillCasted.CastTime - Random.Range(0,3);
                    behaviour = BEHAVIOUR.IDDLE;
                    animator.SetBool(skillCasted.AnimationID, true);
                
                break;

            case BEHAVIOUR.IDDLE:
                if(skillCasted != null)
                {
                    animator.SetBool(skillCasted.AnimationID, false);
                }
                _iddleTimer -= Time.fixedDeltaTime;
                if (_iddleTimer <= 0)
                {
                    behaviour = BEHAVIOUR.ATTACK;
                }
                break;
        }
    }
    IEnumerator PlayCutscene(){   
        GameManager.Instance.PauseGame();
        AsyncOperation op =  SceneManager.LoadSceneAsync("SecondScene", LoadSceneMode.Additive);
        yield return new WaitUntil(() => op.isDone);
        yield return new WaitUntil(() => !GameManager.Instance.inCutscene);
        GameManager.Instance.UnpauseGame();
    }
}
