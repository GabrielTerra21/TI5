using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class Character : MonoBehaviour
{
    [Header("Character Components")]
    public Animator animator;
    public GameObject LockOnTarget;
    public UnityEvent OnDeath, OnDamage;

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
        //GameManager.Instance.pauseGame.AddListener(Pause);
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
        return dmg;
    }

    public virtual void Pause() {
        animator.speed = 0;
    }

    public virtual void Unpause() {
        animator.speed = 0;
    }

    public virtual void Heal(int heal) {
        life += heal;
        if (life > data.maxHp) life = data.maxHp;
    }

    public abstract void Die();

}
