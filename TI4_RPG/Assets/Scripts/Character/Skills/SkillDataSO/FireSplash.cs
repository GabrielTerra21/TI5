using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/FireSplash", order = 2)]

public class FireSplash : SkillDataSO
{
    public override void OnCast(Character from, Character target)
    {
        Instantiate(Prefab,from.transform);
        Collider[] hits = Physics.OverlapSphere(from.transform.position, 3);
        foreach(Collider c in hits)
        {
            if(c.gameObject.TryGetComponent<Character>(out Character character) && !character.CompareTag("Player"))
            {
                character.TakeDamage(Power);
                c.GetComponent<Rigidbody>().AddForce(-c.transform.forward.normalized * 6,ForceMode.Impulse);
            }
        }
    }
}
