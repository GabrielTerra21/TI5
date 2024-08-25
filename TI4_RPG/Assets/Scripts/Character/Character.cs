using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
    [Header("Character Components")]
    public Animator animator;
    public GameObject LockOnTarget;
    public UnityEvent OnDeath, OnDamage,OnHeal;
    public GameObject hitMark;
    [SerializeField] private Material mat;
    [SerializeField] private Shader lit;

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
        mat.shader = lit;
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
        StartCoroutine(Flash(mat));
        
        life -= dmg;
        OnDamage.Invoke();
        if (life <= 0) {
            life = 0;
            Die();
        }
        GameObject particle = Instantiate(hitMark, transform.position, transform.rotation);
        Destroy(particle, 3);
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
        OnHeal.Invoke();
    }

    public abstract void Die();

    IEnumerator Flash(Material mat) {
        mat.shader = Shader.Find("Unlit/DamageShader");
        yield return new WaitForSeconds(0.5f);
        mat.shader = lit;
    }

}
