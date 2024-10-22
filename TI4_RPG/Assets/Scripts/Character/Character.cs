using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class Character : MonoBehaviour
{
    [Header("Character Components")]
    public Animator animator;
    public GameObject LockOnTarget;
    public UnityEvent OnDeath, OnDamage, OnHeal;
    public GameObject hitMark;
    public List<GameObject> dependencies;
    
    [SerializeField] protected Material defaultMat, highlightMat;
    [SerializeField] protected Renderer[] renders;

    [Header("UI Character Elements"), Space(10)]
    [SerializeField] protected DamageText damageText;


    [Space(5)]
    [Header("Character Sheet")]
    public CharacterDataSO data;

    public float moveSpeed;
    public int life;

    private int defense;
    private int defenseBonus = 0;
    private int attack;
    private int attackBonus = 0;

    public bool actionable = true;
    public float count;

    protected virtual void Awake()
    {
        GetData();
        if (renders == null) 
            renders = GetComponentsInChildren<Renderer>();
        if (defaultMat == null)
            defaultMat = renders.First().material;
    }

    protected virtual void Start()
    {
        GameManager.Instance.pauseGame.AddListener(Pause);
        GameManager.Instance.unpauseGame.AddListener(Unpause);
    }

    public void GetData()
    {
        moveSpeed = data.moveSpeed;
        life = data.maxHp;
        defense = data.armor;
        attack = data.power;
    }

    public virtual int TakeDamage(int dmg)
    {
        StartCoroutine(Flash());
        life -= Mathf.Clamp(dmg - Defense(), 0, dmg);
        OnDamage.Invoke(); 
        if (life <= 0)
        {
            life = 0;
            Die();
        }
        GameObject particle = Instantiate(hitMark, transform.position, transform.rotation);
        Destroy(particle, 3);

        if (damageText != null) damageText.DisplayDamage(-dmg + Mathf.Clamp(Defense(), -100, dmg));

        return dmg;
    }

    public int Power() { return attack + attackBonus; }

    public int Defense() { return defense + defenseBonus; }

    public virtual void ApplyBonusAttack(int bonus) { attackBonus += bonus; }

    public virtual void ApplyBonusDefense(int bonus) { defenseBonus += bonus; }

    public virtual void Pause()
    {
        if (animator != null)
            animator.speed = 0;
    }

    public virtual void Unpause()
    {
        if (animator != null)
            animator.speed = 1;
    }

    public virtual void Heal(int heal)
    {
        life += heal;
        if (life > data.maxHp) life = data.maxHp;
        OnHeal.Invoke();
    }

    public abstract void Die();

    IEnumerator Flash() {
        foreach (var data in renders) {
            data.material = highlightMat;
        }

        yield return new WaitForSeconds(0.5f);
        
        foreach (var data in renders) {
            data.material = defaultMat;
        }
    }
    /*
    public void Furia(int power, float castTime) 
    {
        attack += power;
        count = castTime;
    }
    
    
    private void Update()
    {
        if (count > 0 && !GameManager.Instance.paused)
        {
            count -= Time.deltaTime;
        }
        else if(count < 0 && !GameManager.Instance.paused)
        {
            GetData();
        }
    }
    */
}