using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Effect/HealEffect", order = 2)]
public class HealEffect : Effect
{
    public override void DoStuff(List<Character> characters) 
    {
        foreach (Character character in characters)
        {
            if (character.CompareTag("Player"))
            {
                character.Heal(power);
            }
        }
    }
}
