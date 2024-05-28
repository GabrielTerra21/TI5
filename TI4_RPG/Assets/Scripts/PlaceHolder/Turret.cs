using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject prefab;
    public Character from;
    public CombatState state;
    public int power;
    private void Start()
    {
        Destroy(gameObject, 8);
        state = from.GetComponent<CombatState>();
        for(int i = 0; i < 8; i++)
        {
            Invoke("Shoot", 1 * i);
        }
    }
    private void Shoot()
    {
        GameObject g = Instantiate(prefab, this.transform.position, this.transform.rotation);
        Projectile p = g.GetComponent<Projectile>();
        p.damage = power;
        p.target = state.ReturnTarget();
        p.from = from;
    }
}
