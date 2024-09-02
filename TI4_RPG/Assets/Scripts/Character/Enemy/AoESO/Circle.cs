using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AoE/Circle", order = 2)]
public class Circle : AoESO
{
    public override void DealDamage(Vector3 center, int power)
    {
        Collider[] colliders = Physics.OverlapSphere(center, hitbox.x);
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
