using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISkillSlot : EventTrigger {
    
    [Space(10)]
    [Header("Slot Properties")]
    public bool resized;
    public Skill skill;
    public UISkillWheel parent;
    
    [Space(10)]
    [Header("UI Properties of the Slot")]
    public Image image;
    public RectTransform rect;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        resized = false;
    }

    public void UpdateSlot(Skill skill) {
        this.skill = skill;
    }

    private void OnEnable() {
        image.sprite = skill.data.Icon;
        image.type = Image.Type.Filled;
    }

    private void OnDisable() {
        if (resized) {
            rect.sizeDelta -= new Vector2(25, 25);
            resized = false;
        }
    }

    private void FixedUpdate() {
        if (isActiveAndEnabled) image.fillAmount =  (skill.data.CoolDown - skill.cdLeft)/skill.data.CoolDown;
    }

    public override void OnPointerEnter(PointerEventData data) {
        if (skill.ready) {
            Debug.Log($"Hovering over {skill.data.name}");
            rect.sizeDelta += new Vector2(25, 25);
            resized = true;
            parent.selected = skill;
        }
    }
    
    public override void OnPointerExit(PointerEventData data) {
        Debug.Log($"Exited {skill.data.name}");
        if (resized) {
            rect.sizeDelta -= new Vector2(25, 25);
            resized = false;
            parent.selected = null;
        }
    }
}
