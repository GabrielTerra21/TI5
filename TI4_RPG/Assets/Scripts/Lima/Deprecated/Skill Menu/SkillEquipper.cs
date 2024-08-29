using UnityEngine;

public class SkillEquipper : MonoBehaviour {
    public SkillContainer sc;
    public SkillSelectButton[] slots;
    public SkillSelectButton setTo;
    public bool ready;
    


    private void Awake() {
        slots = GetComponentsInChildren<SkillSelectButton>();
        ready = false;
    }

    private void OnEnable() {
        SetSlots();
        //SkillMenuManager.OnSelection += OnSelectSlot;
    }

    private void OnDisable() {
        Equip();
        if(setTo != null)Deselect();
    }

    private void SetSlots() {
        for (int i = 0; i < sc.skills.Length; i++) {
            slots[i].gameObject.SetActive(true);
            if (sc.skills[i].data != null) slots[i].UpdateButton(sc.skills[i].data);
            else slots[i].UpdateButton(GameManager.Instance.empty);
           // slots[i].onSelection += OnSelectSlot;
        }
    }

    // public void OnSelectSlot(SkillSelectButton slot) {
    //     if(slot.transform.parent != transform) return;
    //     if(setTo == slot) Deactivate();
    //     if (setTo != null) setTo.SetDeselected();
    //     setTo = slot;
    // }

    public void Deselect() {
        setTo.SetDeselected();
        setTo = null;
        ready = false;
    }

    public void Select(SkillSelectButton button) {
        if (setTo != null) setTo.SetDeselected();
        setTo = button;
        ready = true;
    }

    private void Equip() {
        for (int i = 0; i < sc.skills.Length; i++) {
            if (slots[i].skill != null) sc.skills[i].data = slots[i].skill;
            else sc.skills[i].data = null;
        }
    }

    public bool Contains(SkillDataSO skill) {
        foreach (var data in slots) {
            if(data.skill == skill) return true;
        }
        return false;
    }

    public void RemoveSkill(SkillDataSO skill) {
        Debug.Log("Removing skill");
        foreach (var data in slots) {
            if (data.skill == skill) {
                data.UpdateButton(GameManager.Instance.empty);
                return;
            }
        }
    }
    
}
