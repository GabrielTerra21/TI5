using UnityEngine;

public class SkillOrganizer : MonoBehaviour {
    [Space(10)]
    [Header("Organizer Components")]
    public SkillInventory inventory;
    public skillSelectButton[] slots;
    public bool ready;
    
    [Space(10)]
    [Header("Functional Properties")]
    public skillSelectButton selected;
    

    private void Awake() {
        slots = GetComponentsInChildren<skillSelectButton>();
    }
    
    private void OnEnable() {
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.skills.Count) {
                slots[i].gameObject.SetActive(true);
                slots[i].UpdateButton(inventory.skills[i]);
                slots[i].onSelection += OnSelect;
            }
            else slots[i].gameObject.SetActive(false);
        }
    }

    private void OnDisable() {
        if (selected) Deselect();
        if (ready) ready = false;
    }

    private void Deselect() {
        selected.SetDeselected();
        selected = null;
    }
    
    private void OnSelect(skillSelectButton selection) {
        if (selected != null) selected.SetDeselected();
        selected = selection;
    }
    
}
