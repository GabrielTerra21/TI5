using UnityEngine;

public class SkillOrganizer : MonoBehaviour {
    [Space(10)]
    [Header("Organizer Components")]
    public SkillInventory inventory;
    public SkillSelectButton[] slots;
    public bool ready;
    [SerializeField] private int length, count;
    
    [Space(10)]
    [Header("Functional Properties")]
    public SkillSelectButton selected;
    

    private void Awake() {
        slots = GetComponentsInChildren<SkillSelectButton>();
        ready = false;
    }
    
    private void OnEnable() {
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.skills.Count) {
                slots[i].gameObject.SetActive(true);
                slots[i].UpdateButton(inventory.skills[i]);
                //slots[i].onSelection += OnSelect;
            }
            else slots[i].gameObject.SetActive(false);
        }
    }

    private void OnDisable() {
        if (selected) Deselect();
        if (ready) ready = false;
    }

    public void Deselect() {
        selected.SetDeselected();
        selected = null;
    }

    public void Select(SkillSelectButton button) {
        if (selected != null) selected.SetDeselected(); 
        selected = button;
        ready = true;
    }
    
}
