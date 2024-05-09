using System.Collections.Generic;
using UnityEngine;

public class SkillOrganizer : MonoBehaviour {
    public SkillInventory inventory;
    //public int width, length;
    public skillSelectButton[] slots;


    private void Awake() {
        slots = GetComponentsInChildren<skillSelectButton>();
    }
    
    private void OnEnable() {
        // for (int i = 0; i < inventory.skills.Count; i++) {
        //     slots[i].skill = inventory.skills[i];
        //     slots[i].gameObject.SetActive(true);
        //     slots[i].UpdateButton();
        // }

        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.skills.Count) {
                slots[i].gameObject.SetActive(true);
                slots[i].UpdateButton(inventory.skills[i]);  
            }
            else slots[i].gameObject.SetActive(false);
        }
    }

    private void UpdateUI() {
        
    }

    private void GetSkills() {
        List<SkillDataSo> list = new List<SkillDataSo>();
        foreach (var data in inventory.skills) {
            list.Add(data);
        }
        list.Sort();
        
        
        //SkillDataSo[,] skillMenu = new SkillDataSo[width, length];
        // for (int i = 0; i < width; i++) {
        //     for (int j = 0; j < length; j++) {
        //         if(list[i + j * width] == null) return skillMenu;
        //         skillMenu[i, j] = list[i + j * width];
        //     }
        // }
        // return skillMenu;
    }
}
