using UnityEngine;

public class Player : Character
{
    [Space(10)]
    [Header("Player Components")]
    [SerializeField] private CCGravity gravity;
    [SerializeField] private SplineLine line;

    
    protected override void Awake(){
        if (GameManager.Instance.player != null) Destroy(gameObject);
        else  GameManager.Instance.player = this; 
        
        base.Awake();
        
        if(gravity == null)gravity = GetComponent<CCGravity>();
        if (!line) line = GetComponentInChildren<SplineLine>();
    }

    protected override void Start() {
        lit = Shader.Find("Particles/Standard Unlit");
        base.Start();
        line.SetOrigin(LockOnTarget);
        line.gameObject.SetActive(false);
    }
    
    private void FixedUpdate(){
        gravity.Gravity();
    }

    public override void Die() {
        CombatState cState = GetComponent<CombatState>();
        GameManager.Instance.CallExploration();
        //cState.OnEndCombat.Invoke();
        
        GameManager.Instance.money = 150;
        GetData();
        
        GameManager.Instance.DeathLoad();
    }

    public void Teleport(Vector3 newPos) {
        Debug.Log("Teleportou");
        CharacterController cc = GetComponent<CharacterController>();
        cc.enabled = false;
        transform.position = newPos;
        cc.enabled = true;
    }

    public void PauseGame() {
        GameManager.Instance.PauseGame();
    }
}
