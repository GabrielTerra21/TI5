using UnityEngine;

public class SkillContainer : MonoBehaviour {
    [SerializeField] private Character owner;
    public Skill autoAttack;
    public Skill[] skills = new Skill[4];

    private void Awake() {
        owner = GetComponent<Character>();
    }

    public void AutoAttack(Character target) {
        autoAttack.OnCast(owner, target);
    }

    public void Cast(int slot, Character target) {
        skills[slot].OnCast(owner,target);
    }
}
