using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillMenuManager : MonoBehaviour {
    public SkillOrganizer organizer;
    public SkillEquipper equipper;
    public static Action<SkillSelectButton> OnSelection;


    private void OnEnable() {
        OnSelection += (Selected) => {
            if(Selected.transform.parent == equipper.transform)equipper.OnSelectSlot(Selected);
            else organizer.OnSelect(Selected);
            Equip();
        };
    }
    
    private void OnDisable() {
        OnSelection = null;
    }

    public void Cancel() {
        gameObject.SetActive(false);
    }
    
    public void Equip() {
        if (organizer.selected && equipper.setTo) {
            equipper.setTo.UpdateButton(organizer.selected.skill);
            organizer.Deselect();
            equipper.Deactivate();
        }
    }
    
}
