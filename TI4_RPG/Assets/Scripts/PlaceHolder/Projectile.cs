using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    enum Type { AreaDamage, SingleTarget }
    [SerializeField]Type type;
    public Character target;
    public float speed;
    Collider[] hits;
    public int damage;
    private void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            transform.position += dir.normalized * speed * Time.deltaTime;
            if (dir.magnitude < 0.5f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        OnHit();
    }
    private void OnHit()
    {
        if (type == Type.AreaDamage)
        {
            hits = Physics.OverlapSphere(transform.position, 4f);
            foreach (Collider c in hits)
            {
                if (c.gameObject.TryGetComponent<Character>(out Character character))
                {
                    character.TakeDamage(damage);
                }
            }
        }
        else
        {
            target.TakeDamage(damage);
        }
    }
}
