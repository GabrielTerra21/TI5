using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skills/Dash", order = 2)]
public class Dash : SkillDataSo
{
    public override void OnCast(Character from, Character target)
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        from.transform.position += dir.normalized * 5;
    }
}
