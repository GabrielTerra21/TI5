using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Effect/ShieldEnemy", order = 2)]
public class ShieldEnemy : Effect
{
    public override void DoStuff(Character character)
    {
        if (!character.TryGetComponent<CrystalEnemy>(out CrystalEnemy component))
        {
            character.ApplyBonusDefense(power);
        }
    }
}
