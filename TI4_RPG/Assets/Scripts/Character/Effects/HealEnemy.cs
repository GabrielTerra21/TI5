using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Effect/HealEnemy", order = 2)]
public class HealEnemy : Effect
{
    public override void DoStuff(Character character)
    {
        if (!character.TryGetComponent<CrystalEnemy>(out CrystalEnemy component))
        {
            character.Heal(power);
        }
    }
}
