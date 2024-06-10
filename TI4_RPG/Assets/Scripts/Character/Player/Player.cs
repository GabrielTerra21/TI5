using System;
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

    private void Start() {
        line.SetOrigin(LockOnTarget);
        line.gameObject.SetActive(false);
    }
    
    private void FixedUpdate(){
        gravity.Gravity();
    }

    public override void Die() {
        Destroy(gameObject);
    }

}
