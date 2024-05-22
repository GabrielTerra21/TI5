using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/FireBall", order = 2)]
public class FireBall : SkillDataSO
{
    public override void OnCast(Character from, Character target)
    {
        GameObject g = Instantiate(Prefab,from.transform.position,from.transform.rotation);
        Projectile p = g.GetComponent<Projectile>();
        p.damage = Power;
        p.target = target;
        p.from = from;
    }
}
