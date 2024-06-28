using UnityEngine;


public class SkillWheel : MonoBehaviour
{
    [SerializeField] private CombatState combatState;
    [SerializeField] GameObject[] skillWheel = new GameObject[2];
    public static SkillWheel instance;
    private GameObject g;
    private int castingSkill;
    private Canvas canvas;
    private Character owner;
    SlotInterface[] slots;

    private void Awake()
    {
        canvas = FindFirstObjectByType<Canvas>();
        combatState = GetComponent<CombatState>();
        owner = combatState.GetComponent<Character>();
        instance = this;
    }
    public void SetCasting(int i)
    {
        castingSkill = i;
    }
    private void Cast(int i)
    {
        if (i == -1) return;
        combatState.Cast(castingSkill);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            slots = FindObjectsOfType<SlotInterface>();
            foreach (SlotInterface slot in slots)
            {
                slot.Initialize(owner);
            }
            g = Instantiate(skillWheel[0]);
            g.transform.SetParent(canvas.transform);
            g.transform.position = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Destroy(g);
            Cast(castingSkill);
        }
        /*if (Input.GetMouseButtonDown(1))
        {
            slots = FindObjectsOfType<SlotInterface>();
            foreach (SlotInterface slot in slots)
            {
                slot.Initialize(owner);
            }
            g = Instantiate(skillWheel[1]);
            g.transform.SetParent(canvas.transform);
            g.transform.position = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Destroy(g);
            Cast(castingSkill);
        }*/
    }
}
