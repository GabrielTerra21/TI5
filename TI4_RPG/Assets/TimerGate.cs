
using System.Collections;
using UnityEngine;

public class TimerGate : Gate {
    private float time;
    public override void Close() {
        StartCoroutine(CloseAfter(time));
    }

    IEnumerator CloseAfter(float cd) {
        float timer = cd;
        while (timer > 0) {
            if (!GameManager.Instance.paused) {
                timer -= Time.deltaTime;
            }
            yield return null;
        }
        base.Close();
    }
}
