using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AoE/Line", order = 2)]
public class Line : AoESO
{
    public Vector3 hitBox;
    public override void DealDamage(Vector3 center,int power)
    {
        Collider[] colliders = Physics.OverlapBox(center, hitBox);
        foreach (Collider c in colliders)
        {
            if (c.CompareTag("Player"))
            {
                c.GetComponent<Character>().TakeDamage(power);
                return;
            }
        }
    }
}
