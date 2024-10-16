using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillMenuManager : MonoBehaviour {
    [Header("UI Elements")]
    public GameObject skillMenuUI;
    
    [Header("Components")]
    public SkillOrganizer organizer;
    public SkillEquipper equipper;
    public PlayerInput playerInput;


    private void OnEnable() {
        playerInput.actions["OpenSkillMenu"].performed += OpenSkillMenu;
    }

    private void OnDisable() {
        playerInput.actions["OpenSkillMenu"].performed -= OpenSkillMenu;
    }

    public void OpenSkillMenu(InputAction.CallbackContext context) {
        if (context.performed && GameManager.Instance.state == GameManager.GameState.EXPLORATION) {
            GameManager.Instance.EnterUI();
            skillMenuUI.SetActive(true);
            playerInput.actions["Cancel"].performed += CloseSkillMenu;
            playerInput.actions["SkillMenuButton"].performed += CloseSkillMenu;
        }
    }
    
    public void CloseSkillMenu(InputAction.CallbackContext context) {
        if (context.performed) {
            playerInput.actions["Cancel"].performed -= CloseSkillMenu;
            playerInput.actions["SkillMenuButton"].performed -= CloseSkillMenu;
            skillMenuUI.SetActive(false);
            GameManager.Instance.ExitUI();
        }
    }

    public void CheckIfReady() {
        if(equipper.ready && organizer.ready) Equip();
    }
    
    private void Equip() {
        Debug.Log("Skill equiped");
        if (equipper.Contains(organizer.selected.skill)) {
            Debug.Log("skill is already equipped");
            equipper.RemoveSkill(organizer.selected.skill);
        }
        equipper.setTo.UpdateButton(organizer.selected.skill);
        organizer.Deselect();
        equipper.Deselect();
    }
    
}
