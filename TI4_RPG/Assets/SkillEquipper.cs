using UnityEngine;

public class SkillEquipper : MonoBehaviour {
    public SkillContainer sc;
    public EquipSlot[] slots;
    public EquipSlot setTo;
    public bool ready;
    


    private void Awake() {
        slots = GetComponentsInChildren<EquipSlot>();
    }

    private void OnEnable() {
        SetSlots();
    }

    private void SetSlots() {
        for (int i = 0; i < sc.skills.Length; i++) {
            if (sc.skills[i].data != null) {
                slots[i].gameObject.SetActive(true);
                slots[i].UpdateSkillSlot(sc.skills[i].data);
                slots[i].onSetSpace += OnSelectSlot;
            }
            else slots[i].gameObject.SetActive(false);
        }
    }

    public void OnSelectSlot(EquipSlot slot) {
        if(setTo == slot) Deactivate();
        if (setTo != null) setTo.SetInactive();
        setTo = slot;
    }

    public void Deactivate() {
        setTo.SetInactive();
        setTo = null;
    }

    private void Equip() {
        for (int i = 0; i < sc.skills.Length; i++) {
            if (slots[i].skill != null) sc.skills[i].data = slots[i].skill;
            else sc.skills[i].data = null;
        }
    }
    
    
}
