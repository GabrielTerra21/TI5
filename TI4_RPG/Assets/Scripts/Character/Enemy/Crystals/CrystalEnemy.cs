using System.Collections.Generic;
using UnityEngine;

public class CrystalEnemy : Enemy
{
    public Effect effect, deathEffect;
    float countLoop = 0;
    public GameObject particles;
    public List<Enemy> enemies;

    public enum Type {Continuos, Instant}
    public Type type;

    protected override void Start()
    {
        base.Start();
        if(type == Type.Instant)
        {
            foreach(Enemy enemy in enemies)
            {
                effect.DoStuff(enemy);
            }
        }
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.paused && this.type == Type.Continuos)
        {
            countLoop += Time.fixedDeltaTime;
            if (countLoop >= effect.interval)
            {
                countLoop = 0;
                foreach (Character character in enemies)
                {
                    effect.DoStuff(character);
                }
                Instantiate(particles,transform.position,transform.rotation);
            }
        }
    }
    public void OnDeathEffect()
    {
        foreach (Enemy enemy in enemies)
        {
            deathEffect.DoStuff(enemy);
        }
    }
    public override void Die()
    {
        if(deathEffect != null)
        {
            OnDeathEffect();
        }
        base.Die();
    }
}
