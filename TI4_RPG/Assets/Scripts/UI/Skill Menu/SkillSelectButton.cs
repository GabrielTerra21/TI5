using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSelectButton : EventTrigger {
    [Space(10)]
    [Header("UI Components")]
    public Image icon, background;
    public RectTransform rect;
    
    [Space(10)]
    [Header("Functional Components")]
    public SkillDataSO skill;
    public bool resized, selected;
    public Color normalCol;


    private void Awake() {
        rect = GetComponent<RectTransform>();
        icon = GetComponent<Image>();
        background = GetComponentInChildren<Image>();
    }

    private void Start() {
        normalCol = icon.color;
        resized = false;
        selected = false;
    }
    
    public void UpdateButton(SkillDataSO skill) {
        this.skill = skill;
        if(icon == null) icon = GetComponent<Image>();
        icon.sprite = this.skill.Icon;
        if(selected) SetDeselected();
    }
    
    private void OnDisable() {
        Shrink();
    }

    // public override void OnPointerClick(PointerEventData eventData) {
    //     if(selected) return;
    //     SetSelected();
    //     SkillMenuManager.OnSelection?.Invoke(this);
    //     parent.Select(this);
    // }

    public void Grow() {
        if(selected) return;
        
        rect.sizeDelta += new Vector2(25, 25);
        resized = true;
    }

    public void Shrink() {
        Debug.Log("entered");
        if(selected) return;
        Debug.Log("did");
        if (resized) {
            rect.sizeDelta -= new Vector2(25, 25);
            resized = false;
        }
    }

    public void SetSelected() {
        Debug.Log("selected");
        if(background == null) background = GetComponentInChildren<Image>();
        if (icon == null) icon = GetComponent<Image>();
        selected = true;
        background.color = Color.yellow;
        icon.color = Color.yellow;
    }

    public void SetDeselected() {
        Debug.Log("Deselected");
        if(background == null) background = GetComponentInChildren<Image>();
        if (icon == null) icon = GetComponent<Image>();
        selected = false;
        Shrink();
        icon.color = normalCol;
        background.color = normalCol;
    }
}
