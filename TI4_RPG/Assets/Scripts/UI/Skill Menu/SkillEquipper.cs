using System;
using UnityEngine;

public class SkillEquipper : MonoBehaviour {
    public SkillContainer sc;
    public SkillSelectButton[] slots;
    public SkillSelectButton setTo;
    public bool ready;
    


    private void Awake() {
        slots = GetComponentsInChildren<SkillSelectButton>();
    }

    private void OnEnable() {
        SetSlots();
        //SkillMenuManager.OnSelection += OnSelectSlot;
    }

    private void OnDisable() {
        Equip();
        if(setTo)Deactivate();
    }

    private void SetSlots() {
        for (int i = 0; i < sc.skills.Length; i++) {
            slots[i].gameObject.SetActive(true);
            if (sc.skills[i].data != null) slots[i].UpdateButton(sc.skills[i].data);
            else slots[i].UpdateButton(GameManager.Insatance.empty);
           // slots[i].onSelection += OnSelectSlot;
        }
    }

    public void OnSelectSlot(SkillSelectButton slot) {
        if(slot.transform.parent != transform) return;
        //if(setTo == slot) Deactivate();
        if (setTo != null) setTo.SetDeselected();
        setTo = slot;
    }

    public void Deactivate() {
        setTo.SetDeselected();
        setTo = null;
    }

    private void Equip() {
        for (int i = 0; i < sc.skills.Length; i++) {
            if (slots[i].skill != null) sc.skills[i].data = slots[i].skill;
            else sc.skills[i].data = null;
        }
    }
    
    
}
