using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AoE/Line", order = 2)]
public class Line : AoESO
{
    public override void DealDamage(Vector3 center,int power)
    {
        Collider[] colliders = Physics.OverlapBox(center, hitbox);
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
