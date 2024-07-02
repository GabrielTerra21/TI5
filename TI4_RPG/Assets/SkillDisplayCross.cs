using System.Collections;
using UnityEngine;

public class SkillDisplayCross : MonoBehaviour {
    [Header("References")]
    public CombatState player;
    public SkillContainer source;
    
    [Header("Components")]
    public DisplaySkill[] slots;
    
    private void Awake() {
        source = player.skillManager;
        if (GameManager.Instance == null)  StartCoroutine(SetCross()); 
        else GameManager.Instance.cross = this;

    }
    
    private void Start() {
        UpdateSlots();
    }
    
    public void UpdateSlots() {
        for (int i = 0; i < source.skills.Length; i++) {
            if (source.skills[i] != null) {
                slots[i].UpdateSlot(source.skills[i]);
                slots[i].gameObject.SetActive(true);
            }
            else slots[i].gameObject.SetActive(false);
        }
    }


    IEnumerator SetCross() {
        yield return new WaitUntil(() => GameManager.Instance != null);
        GameManager.Instance.cross = this;
    }
}
