using System.Collections.Generic;
using UnityEngine;

public class SkillInventory : MonoBehaviour {
    public List<SkillDataSO> skills = new List<SkillDataSO>();


    private void Awake() {
        skills.Sort();
    }
    
    public void Learn(SkillDataSO learned) {
        skills.Add(learned);
        //skills.Sort();
    }
}
