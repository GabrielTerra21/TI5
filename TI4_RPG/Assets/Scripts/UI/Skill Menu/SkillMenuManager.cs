using UnityEngine;
using UnityEngine.InputSystem;

public class SkillMenuManager : MonoBehaviour {
    public GameObject skillMenuUI;
    public SkillOrganizer organizer;
    public SkillEquipper equipper;


    public void OpenSkillMenu() {
        GameManager.Instance.EnterUI();
        skillMenuUI.SetActive(true);
        GameManager.Instance.playerInput.actions["Cancel"].performed += CloseSkillMenu;
        GameManager.Instance.playerInput.actions["SkillMenuButton"].performed += CloseSkillMenu;
    }
    
    public void CloseSkillMenu(InputAction.CallbackContext context) {
        GameManager.Instance.playerInput.actions["Cancel"].performed -= CloseSkillMenu;
        GameManager.Instance.playerInput.actions["SkillMenuButton"].performed -= CloseSkillMenu;
        GameManager.Instance.ExitUI();
        skillMenuUI.SetActive(false);
    }

    public void CheckIfReady() {
        if(equipper.ready && organizer.ready) Equip();
    }
    
    private void Equip() {
        Debug.Log("Skill equiped");
        equipper.setTo.UpdateButton(organizer.selected.skill);
        organizer.Deselect();
        equipper.Deactivate();
    }
    
}
