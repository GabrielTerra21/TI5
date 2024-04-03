using System;
using UnityEngine;

public class Player : Character
{
    [Space(10)]
    [Header("Player Components")]
    public CCGravity gravity;

    
    protected override void Awake(){
        base.Awake();
        gravity = GetComponent<CCGravity>();
    }
    
    private void FixedUpdate(){
        gravity.Gravity();
    }

    public override void Die() {
        throw new NotImplementedException();
    }

}
