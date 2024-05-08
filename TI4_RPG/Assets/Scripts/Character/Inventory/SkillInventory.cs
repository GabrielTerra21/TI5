using System.Collections.Generic;
using UnityEngine;

public class SkillInventory : MonoBehaviour {
    public List<SkillDataSo> skills = new List<SkillDataSo>();


    public void Learn(SkillDataSo learned) {
        skills.Add(learned);
        skills.Sort();
    }
}
