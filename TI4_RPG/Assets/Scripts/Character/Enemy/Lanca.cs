using UnityEngine;

public class Lanca : Enemy
{

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Pause()
    {
        base.Pause();
    }

    public override void Unpause()
    {
        base.Unpause();
    }
    protected override void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        Debug.Log("Morri XD");
        OnDeath.Invoke();
        Destroy(gameObject);
        GameManager.Instance.player.GetComponent<CombatState>().FindEnemys();
    }
}
