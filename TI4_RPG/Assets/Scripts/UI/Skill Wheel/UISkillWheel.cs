using UnityEngine;
using UnityEngine.InputSystem;

public class UISkillWheel : MonoBehaviour {
    public CombatState player;
    public Exploring exploring;
    public GameObject wheel;
    public SkillContainer sc;
    public UISkillSlot[] slots;
    public Skill selected;


    private void Awake() {
        sc = player.skillManager;
        foreach (var slot in slots) slot.parent = this;
    }

    public void OnSkillWheel(InputAction.CallbackContext context) {
        if (context.started) {
            Debug.Log("Button has been pressed");
            RectTransform rect = wheel.GetComponent<RectTransform>();
            rect.position = Mouse.current.position.ReadValue();
            UpdateSlots();
            wheel.SetActive(true);
        }

        if (context.canceled) {
            wheel.SetActive(false);
            if(selected != null)Cast(selected);
            selected = null;
        }
    }

    public void UpdateSlots() {
        for (int i = 0; i < sc.skills.Length; i++) {
            if (sc.skills[i] != null) {
                slots[i].UpdateSlot(sc.skills[i]);
                slots[i].gameObject.SetActive(true);
            }
            else slots[i].gameObject.SetActive(false);
        }
    }

    public void Cast(Skill skill) {
        Debug.Log($"Selected {skill.data.name}");
        exploring.OutCombtCast(skill);
        player.Cast(skill);
    }
}
