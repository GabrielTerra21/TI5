using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Effect/ApEffect", order = 2)]
public class ApEffect : Effect
{
    public override void DoStuff(Character character)
    {
        if (character.CompareTag("Player"))
        {
            GameManager.Instance.GainAP(power);
        }
    }
}
