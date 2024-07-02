using UnityEngine;

public class Player : Character
{
    [Space(10)]
    [Header("Player Components")]
    [SerializeField] private CCGravity gravity;
    [SerializeField] private SplineLine line;

    
    protected override void Awake(){
        base.Awake();
        if(gravity == null)gravity = GetComponent<CCGravity>();
        if (!line) line = GetComponentInChildren<SplineLine>();
    }

    protected override void Start() {
        base.Start();
        line.SetOrigin(LockOnTarget);
        line.gameObject.SetActive(false);
    }
    
    private void FixedUpdate(){
        gravity.Gravity();
    }

    public override void Die() {
        Destroy(gameObject);
        GameManager.Instance.LoadNewScene("Derrota");
        GameManager.Instance.money = 150;
    }

    public void PauseGame() {
        GameManager.Instance.PauseGame();
    }
}
