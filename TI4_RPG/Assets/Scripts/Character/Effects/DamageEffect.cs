using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Effect/DamageEffect", order = 2)]
public class DamageEffect : Effect
{
    public override void DoStuff(Character character)
    {
        if (character.CompareTag("Player"))
        {
            character.TakeDamage(character.Defense() + power);
        }
    }
}

