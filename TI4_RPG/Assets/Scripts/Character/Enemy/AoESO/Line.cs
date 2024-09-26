using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AoE/Line", order = 2)]
public class Line : AoESO
{
    /*public override void DealDamage(Vector3 center,int power,Quaternion rotation)
    {
        Collider[] colliders = Physics.OverlapBox(centerTest, hitbox, rotation);
        foreach (Collider c in colliders)
        {
            if (c.CompareTag("Player"))
            {
                c.GetComponent<Character>().TakeDamage(power);
                GameManager.Instance.LoseAP(apDamage);
                return;
            }
        }
    }*/
}
