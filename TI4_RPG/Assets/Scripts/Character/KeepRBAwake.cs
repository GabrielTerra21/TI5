using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRBAwake : MonoBehaviour {
    public Rigidbody rb;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (rb.IsSleeping()) rb.WakeUp();
    }
}
