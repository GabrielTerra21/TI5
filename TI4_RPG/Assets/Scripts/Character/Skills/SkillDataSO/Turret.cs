using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject projectile;
    public Character from;
    public CombatState state;
    public int power;
    private void Start()
    {
        Destroy(gameObject, 8);
        state = from.GetComponent<CombatState>();
        InvokeRepeating("Shoot", 0, 1f);
    }
    private void Shoot()
    {
        GameObject g = Instantiate(projectile, transform.position, transform.rotation);
        Projectile p = g.GetComponent<Projectile>();
        p.damage = power;
        p.target = state.ReturnTarget();
        p.from = from;
    }
}
