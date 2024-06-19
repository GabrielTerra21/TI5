using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillMenuManager : MonoBehaviour {
    public GameObject skillMenuUI;
    public SkillOrganizer organizer;
    public SkillEquipper equipper;
    public static Action<SkillSelectButton> OnSelection;


    public void OpenSkillMenu() {
        GameManager.Instance.EnterUI();
        skillMenuUI.SetActive(true);
        GameManager.Instance.playerInput.actions["Cancel"].performed += CloseSkillMenu;
        
        OnSelection += (Selected) => {
            if(Selected.transform.parent == equipper.transform)equipper.OnSelectSlot(Selected);
            else organizer.OnSelect(Selected);
            Equip();
        };
    }
    
    public void CloseSkillMenu(InputAction.CallbackContext context) {
        OnSelection = null;
        GameManager.Instance.playerInput.actions["Cancel"].performed -= CloseSkillMenu;
        GameManager.Instance.ExitUI();
        skillMenuUI.SetActive(false);
    }
    
    public void Equip() {
        if (organizer.selected && equipper.setTo) {
            equipper.setTo.UpdateButton(organizer.selected.skill);
            organizer.Deselect();
            equipper.Deactivate();
        }
    }
    
}
