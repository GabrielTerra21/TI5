using System.Collections.Generic;
using UnityEngine;

public class SkillInventory : MonoBehaviour {
    public List<SkillDataSo> skills = new List<SkillDataSo>();


    private void Awake() {
        skills.Sort();
    }
    
    public void Learn(SkillDataSo learned) {
        skills.Add(learned);
        skills.Sort();
    }
}
