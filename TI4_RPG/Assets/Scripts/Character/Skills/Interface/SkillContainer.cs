using UnityEngine;

public class SkillContainer : MonoBehaviour {
    [SerializeField] private Character owner;
    public Skill autoAttack;
    public Skill[] skills = new Skill[4];
    public GameObject genericTarget;
    public Transform genericTransform;
    //public SkillInventory inventory;

    private void Awake() {
        owner = GetComponent<Character>();
        //inventory.GetComponent<SkillInventory>();
        //UpdateInventory();
    }

    public void AutoAttack(Character target) {
        autoAttack.OnCast(owner, target);
    }

    public void Cast(int slot, Character target = null) {
        if (target == null)
        {
            target = Instantiate(genericTarget,genericTransform.position,genericTransform.rotation).GetComponent<Character>();
            target.transform.parent = null;
            skills[slot].OnCast(owner, target);
            Destroy(target, 8f);
        }
        skills[slot].OnCast(owner,target);
    }

    public void Cast(Skill skill, Character target = null) {
        if (target == null)
        {
            Debug.Log("CastGeneric");
            target = Instantiate(genericTarget, genericTransform).GetComponent<Character>();
            target.transform.parent = null;
            skill.OnCast(owner, target);
            Destroy(target,8f);
        }
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
