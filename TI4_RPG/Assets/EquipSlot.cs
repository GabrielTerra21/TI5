using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipSlot : EventTrigger {
    [Space(10)] [Header("EquipSlot Components")]
    public Image icon;
    public SkillDataSo skill;

    [Space(10)] [Header("EquipSlot Functional Properties")]
    public Color baseColor, selectedColor;
    public RectTransform rect;
    public bool active,resized;
    public Action<EquipSlot> onSetSpace;


    private void Awake() {
        icon = GetComponent<Image>();
        baseColor = icon.color;
        rect = GetComponent<RectTransform>();
        resized = false;
        active = false;
    }
    
    public void UpdateSkillSlot(SkillDataSo setSkill) {
        skill = setSkill;
        icon.sprite = skill.Icon;
    }

    public override void OnPointerEnter(PointerEventData pointerEvent) {
        if (!active) Grow();
    }

    public override void OnPointerExit(PointerEventData pointerEvent) {
        if (!active) Shrink();
    }

    public override void OnPointerClick(PointerEventData pointerEventData) {
        onSetSpace?.Invoke(this);
        SetActive();
    }

    public void SetActive() {
        active = true;
        icon.color = selectedColor;
    }

    public void SetInactive() {
        Shrink();
        active = false;
        icon.color = baseColor;
    }
    public void Grow() {
        rect.sizeDelta += new Vector2(25, 25);
        resized = true;
    }

    public void Shrink() {
        if (resized) {
            rect.sizeDelta -= new Vector2(25, 25);
            resized = false;
        }
    }
    
}
