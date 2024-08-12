using UnityEngine;

public class SkillManager : MonoBehaviour {
    
    [SerializeField] private Character owner;
    public Skill autoAttack;
    public SkillDataSO[] skills = new SkillDataSO[6];

    private void Awake() {
        owner = GetComponent<Character>();
    }

    public void AutoAttack(Character target) {
        autoAttack.OnCast(owner, target);
    }

    public void Cast(int slot, Character target = null) {
        skills[slot].OnCast(owner,target);
    }

    public void Cast(Skill skill, Character target = null) {
        skill.OnCast(owner, target);
    }
}
