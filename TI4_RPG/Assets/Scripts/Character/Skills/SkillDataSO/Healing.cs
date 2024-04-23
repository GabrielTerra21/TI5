using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/Heal", order = 2)]
public class Healing : SkillDataSo
{
    public override void OnCast(Character from, Character target)
    {
        Instantiate(Prefab, from.transform);
        from.Heal(20);
    }
}
