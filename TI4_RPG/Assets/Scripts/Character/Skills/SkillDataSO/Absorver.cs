using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/Absorver", order = 2)]
public class Absorver : SkillDataSO
{
    int heal;
    public override void OnCast(Character from, Character target)
    {
        
        //Instantiate(Prefab, from.transform);
        Collider[] hits = Physics.OverlapSphere(from.transform.position, 3);
        foreach (Collider c in hits)
        {
            if (c.gameObject.TryGetComponent<Character>(out Character character) && !character.CompareTag("Player"))
            {
                heal += character.TakeDamage(Power);
            }
        }
        from.Heal(heal/2);
    }
}
