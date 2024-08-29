
using System.Collections;
using UnityEngine;

public class TimerGate : Gate {
    public float time;
    public bool isOpen;

    public void Start() {
        isOpen = false;
    }

    public override void Open() {
        StopAllCoroutines();
        base.Open();
    }
    
    public override void Close() {
        StartCoroutine(CloseAfter(time));
    }

    IEnumerator CloseAfter(float cd) {
        if (isOpen) {
            yield break;
        }
        isOpen = true;
        float timer = cd;
        while (timer > 0) {
            if (!GameManager.Instance.paused) {
                timer -= Time.deltaTime;
            }
            yield return null;
        }
        base.Close();
        isOpen = false;
    }
}
