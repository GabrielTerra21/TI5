using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/Skills/Empty")]
public class Empty : SkillDataSO
{
    public override void OnCast(Character from, Character target) {
        Debug.Log("this is an empty skill slot");
    }
}
