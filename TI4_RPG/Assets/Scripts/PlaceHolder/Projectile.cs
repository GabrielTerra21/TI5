using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    Collider[] hits;
    public int damage;
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;
        if(dir.magnitude < 0.5f)
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
        hits = Physics.OverlapSphere(transform.position, 4f);
        foreach (Collider c in hits)
        {
            if(c.gameObject.TryGetComponent<Character>(out Character character))
            {
                character.TakeDamage(damage);
            }
        }
    }
}
