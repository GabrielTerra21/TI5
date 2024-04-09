using System;
using UnityEngine;

public class Player : Character
{
    [Space(10)]
    [Header("Player Components")]
    [SerializeField] private CCGravity gravity;

    
    protected override void Awake(){
        base.Awake();
        if(gravity == null)gravity = GetComponent<CCGravity>();
    }
    
    private void FixedUpdate(){
        gravity.Gravity();
    }

    public override void Die() {
        throw new NotImplementedException();
    }

}
