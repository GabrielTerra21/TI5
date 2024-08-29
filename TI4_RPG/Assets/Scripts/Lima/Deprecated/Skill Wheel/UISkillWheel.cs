using UnityEngine;
using UnityEngine.InputSystem;

public class UISkillWheel : MonoBehaviour {
    public CombatState player;
    public Exploring exploring;
    public GameObject wheel;
    public SkillContainer sc;
    public UISkillSlot[] slots;
    public Skill selected;
    public PlayerInput playerInput;


    private void Awake() {
        //sc = player.skillManager;
        foreach (var slot in slots) slot.parent = this;
    }

    private void Start() {
        UpdateSlots();
    }

    private void OnEnable() {
        playerInput.actions["SkillWheel"].started += OnActivateSkillWheel;
        playerInput.actions["SkillWheel"].canceled += OnReleaseSkillWheel;
    }
    
    private void OnDisable(){ 
        playerInput.actions["SkillWheel"].started -= OnActivateSkillWheel;
        playerInput.actions["SkillWheel"].canceled -= OnReleaseSkillWheel;
    }
    
    public void OnActivateSkillWheel(InputAction.CallbackContext context) {
        Debug.Log("Button has been pressed");
        RectTransform rect = wheel.GetComponent<RectTransform>();
        rect.position = Mouse.current.position.ReadValue();
        UpdateSlots();
        wheel.SetActive(true);
    }

    public void OnReleaseSkillWheel(InputAction.CallbackContext context) {
        wheel.SetActive(false);
        if(selected != null)Cast(selected);
        selected = null;
    }
    
    /*
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
    */

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
        if (GameManager.Instance.state == GameManager.GameState.EXPLORATION)
        {
            exploring.OutCombtCast(skill);
        }
        else
        {
           // player.Cast(skill);
        }
    }
}
