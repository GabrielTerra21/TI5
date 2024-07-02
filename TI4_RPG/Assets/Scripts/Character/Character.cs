using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
    [Header("Character Components")]
    public Animator animator;
    public GameObject LockOnTarget;
    public UnityEvent OnDeath, OnDamage,OnHeal;
    public GameObject hitMark;

    [Space(5)]
    [Header("Character Sheet")]
    public CharacterDataSO data;
    public float moveSpeed;
    public int life;
    public int defense { get; private set; }
    public int attack { get; private set; }
    public bool actionable = true;

    protected virtual void Awake() {
        GetData();
    }

    protected virtual void Start() {
        GameManager.Instance.pauseGame.AddListener(Pause);
        GameManager.Instance.unpauseGame.AddListener(Unpause);
    }

    public void GetData(){
        moveSpeed = data.moveSpeed;
        life = data.maxHp;
        defense = data.armor;
        attack = data.power;
    }

    public virtual int TakeDamage(int dmg) {
        life -= dmg;
        OnDamage.Invoke();
        if (life <= 0) {
            life = 0;
            Die();
        }
        Instantiate(hitMark, transform.position, transform.rotation);
        return dmg;
    }

    public virtual void Pause() {
        if(animator != null) 
        animator.speed = 0;
    }

    public virtual void Unpause() {
        if (animator != null)
            animator.speed = 1;
    }

    public virtual void Heal(int heal) {
        life += heal;
        if (life > data.maxHp) life = data.maxHp;
    }

    public abstract void Die();

}
