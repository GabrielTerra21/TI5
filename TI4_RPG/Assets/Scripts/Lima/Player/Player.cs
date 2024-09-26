using System.Collections;
using UnityEngine;

public class Player : Character
{
    [Space(10)]
    [Header("Player Components")]
    [SerializeField] private CCGravity gravity;
    [SerializeField] private SplineLine line;
    [SerializeField] private SkinnedMeshRenderer[] renderers;
    [SerializeField] protected DamageText defenseText;
    public SkillDataSO furia;


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
        {
            furia.OnCast(this, null);
        }
            
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
        life -= dmg - Defense();
        OnDamage.Invoke();
        if (life <= 0) {
            life = 0;
            Die();
        }
        GameObject particle = Instantiate(hitMark, transform.position, transform.rotation);
        Destroy(particle, 3);
        damageText.DisplayDamage(-dmg + Defense());
        defenseText.DisplayDamage(Defense());
        return dmg;
    }
    
    // Executa o codigo base de apllicação de bonus para o status de ataque.
    // Chama a função do GameManager para retornar o proximo slot de status disponivel (atualamente sem display) 
    // Verifica se o bonus foi positivo ou negativo, caso seja positivo, faz o display da versão buff do status, caso contrario,
    // faz o display o a versão debuff do status.
    public override void ApplyBonusAttack(int bonus) {
        base.ApplyBonusAttack(bonus);
        StatusDisplay slot = GameManager.Instance.GetStatusSlot();
        if (bonus > 0) 
            slot?.DisplayStatus(StatusDisplay.statusEffect.ATTACKUP);
        else 
            slot?.DisplayStatus(StatusDisplay.statusEffect.ATTACKDOWN);
    }
    
    // Executa o codigo base de apllicação de bonus para o status de defesa.
    // Chama a função do GameManager para retornar o proximo slot de status disponivel (atualamente sem display) 
    // Verifica se o bonus foi positivo ou negativo, caso seja positivo, faz o display da versão buff do status, caso contrario,
    // faz o display o a versão debuff do status.
    public override void ApplyBonusDefense(int bonus) {
        base.ApplyBonusDefense(bonus);
        StatusDisplay slot = GameManager.Instance.GetStatusSlot();
        if(bonus > 0) 
            slot?.DisplayStatus(StatusDisplay.statusEffect.DEFUP);
        else 
            slot?.DisplayStatus(StatusDisplay.statusEffect.DEFDOWN);
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
