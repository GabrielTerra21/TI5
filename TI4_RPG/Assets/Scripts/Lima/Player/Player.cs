using System.Collections;
using UnityEngine;

public class Player : Character
{
    [Space(10)]
    [Header("Player Components")]
    [SerializeField] private CCGravity gravity;
    [SerializeField] private SplineLine line;
    [SerializeField] private SkinnedMeshRenderer[] renderers;

    
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
        if (Input.GetKeyDown(KeyCode.L))
            TakeDamage(5);
    }

    public override void Die() {
        CombatState cState = GetComponent<CombatState>();
        GameManager.Instance.CallExploration();
        //cState.OnEndCombat.Invoke();
        
        GameManager.Instance.ecos = 150;
        GetData();
        
        GameManager.Instance.DeathLoad();
    }

    public override int TakeDamage(int dmg) {
        StartCoroutine(Flashing(renderers));
        life -= dmg;
        OnDamage.Invoke();
        if (life <= 0) {
            life = 0;
            Die();
        }
        GameObject particle = Instantiate(hitMark, transform.position, transform.rotation);
        Destroy(particle, 3);
        damageText.DisplayDamage(-dmg);
        return dmg;
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

    IEnumerator Flashing(SkinnedMeshRenderer[] mats) {
        foreach (var data in mats) {
            data.material.shader = Shader.Find("Unlit/DamageShader");
        }
        yield return new WaitForSeconds(0.5f);
        foreach (var data in mats) {
            data.material.shader = Shader.Find("Universal Render Pipeline/Lit");
        }
    }
}
