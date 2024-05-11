using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class skillSelectButton : EventTrigger {
    [Space(10)]
    [Header("UI Components")]
    public Image icon;
    public RectTransform rect;
    
    [Space(10)]
    [Header("Functional Components")]
    public SkillDataSo skill;
    public bool resized, selected;
    public Color normalCol, selectedCol;
    public Action<skillSelectButton> onSelection;


    private void Awake() {
        rect = GetComponent<RectTransform>();
        icon = GetComponent<Image>();
        normalCol = icon.color;
        resized = false;
        selected = false;
    }
    
    public void UpdateButton(SkillDataSo skill) {
        this.skill = skill;
        icon.sprite = this.skill.Icon;
    }

    private void OnDisable() {
        Shrink();
        onSelection = null;
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        if(!selected) Grow();
    }

    public override void OnPointerExit(PointerEventData eventData) {
        if(!selected) Shrink();
    }

    public override void OnPointerClick(PointerEventData eventData) {
        if(selected) return;
        onSelection?.Invoke(this);
        SetSelected();
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

    public void SetSelected() {
        selected = true;
        icon.color = selectedCol;
    }

    public void SetDeselected() {
        Shrink();
        selected = false;
        icon.color = normalCol;
    }
    
}
