using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public List<Character> characters = new List<Character>();
    public EnemyDisplay[] slots = new EnemyDisplay[3];


    public void UpdateSlots(){
        for(int i = 0; i < slots.Length; i++){
            slots[i].owner = characters[i];
        }
    }
}
