using UnityEngine;

public class SkillContainer : MonoBehaviour {
    [SerializeField] private Character owner;
    public Skill autoAttack;
    public Skill[] skills = new Skill[4];
    public Character genericTarget;
    //public SkillInventory inventory;

    private void Awake() {
        owner = GetComponent<Character>();
        //inventory.GetComponent<SkillInventory>();
        //UpdateInventory();
    }

    public void AutoAttack(Character target) {
        autoAttack.OnCast(owner, target);
    }

    public void Cast(int slot, Character target) {

        skills[slot].OnCast(owner,target);
    }

    public void Cast(Skill skill, Character target) {
        skill.OnCast(owner, target);
    }
    
    // public void UpdateInventory() {
    //     for (int i = 0; i < skills.Length; i++) {
    //         if (skills[i].data != null) {
    //             inventory.skills[i] = skills[i].data;
    //         }
    //     }
    // }

    // public void UpdateEquippedSkills() {
    //     for (int i = 0; i < skills.Length; i++) {
    //         if (inventory.skills[i] != null) skills[i].data = inventory.skills[i];
    //         else skills[i].data = null;
    //     }
    // }
}
