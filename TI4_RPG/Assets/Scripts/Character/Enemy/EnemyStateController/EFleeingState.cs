using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EFleeingState : State {
    public Vector3 homePos;

    
    public override State OnEnterState() {
        return this;
    }

    public override void OnExitState() {
        
    }
    
}
