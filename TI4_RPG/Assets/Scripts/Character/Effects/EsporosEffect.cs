using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Effect/EsporosEffect", order = 2)]
public class EsporosEffect : Effect
{
    public override void DoStuff(List<Character> characters)
    {
        foreach (Character character in characters)
        {
            if (character.CompareTag("Player"))
            {
                GameManager.Instance.LoseAP(power);
            }
        }
    }
}
