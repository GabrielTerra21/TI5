using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/CharacterData", order = 1)]
public class CharacterDataSO : ScriptableObject
{
    public int MaxHp;
    public string CharName;
    public AnimationController animationController;
}
