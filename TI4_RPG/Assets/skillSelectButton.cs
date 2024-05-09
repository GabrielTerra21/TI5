using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class skillSelectButton : EventTrigger {
    [Space(10)]
    [Header("UI Components")]
    public Image icon;
    public RectTransform rect;
    
    [Space(10)]
    [Header("Functional Components")]
    public SkillDataSo skill;
    public bool resized;


    private void Awake() {
        rect = GetComponent<RectTransform>();
        icon = GetComponent<Image>();
        resized = false;
    }
    
    public void UpdateButton(SkillDataSo skill) {
        this.skill = skill;
        icon.sprite = this.skill.Icon;
    }

    private void OnDisable() {
        if (resized) {
            rect.sizeDelta -= new Vector2(25, 25);
            resized = false;
        }
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        rect.sizeDelta += new Vector2(25, 25);
        resized = true;
    }

    public override void OnPointerExit(PointerEventData eventData) {
        if (resized) {
            rect.sizeDelta -= new Vector2(25, 25);
            resized = false;
        }
    }
}
