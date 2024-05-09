using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillOrganizer : MonoBehaviour {
    public SkillInventory inventory;
    public int width, length;
    public Image[] icons;


    private void OnEnable() {
        
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
