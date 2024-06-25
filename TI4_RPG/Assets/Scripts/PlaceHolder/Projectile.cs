using UnityEngine;

public class Projectile : MonoBehaviour
{
    enum Type { AreaDamage, SingleTarget }
    [SerializeField]Type type;
    public Character target;
    public Character from;
    public float speed;
    Collider[] hits;
    
    public int damage;
    private void Update()
    {
        if(GameManager.Instance.paused) return;
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
            target = from.GetComponent<CombatState>().ReturnTarget();
            if(target == null)
            {
                Destroy(gameObject);
            }
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
            foreach (Collider col in hits)
            {
                if (col.gameObject.TryGetComponent<Character>(out Character character) && character != from)
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
