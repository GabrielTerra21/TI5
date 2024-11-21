using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/FireSplash", order = 2)]

public class FireSplash : SkillDataSO
{
    public override void OnCast(Character from, Character target)
    {
        Instantiate(Prefab,from.transform);
        Collider[] hits = Physics.OverlapSphere(from.transform.position, 3);
        foreach(Collider col in hits)
        {
            if(col.gameObject.TryGetComponent<Character>(out Character character) && character != from)
            {
                character.TakeDamage(Power);
                //col.GetComponent<Rigidbody>().AddForce(-col.transform.forward.normalized * 6,ForceMode.Impulse);
            }
        }
    }
}
