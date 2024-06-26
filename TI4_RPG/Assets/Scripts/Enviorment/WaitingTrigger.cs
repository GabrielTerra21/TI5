using UnityEngine;

public class WaitingTrigger : Trigger
{
    private void OnTriggerEnter(Collider other) {
        //if (other.CompareTag("Player")) { other.GetComponent<Exploring>().waitingTriggers.Add(this); }
        if (other.CompareTag("Player") && GameManager.Instance.state == GameManager.GameState.EXPLORATION) {
            other.GetComponent<Exploring>().interact = () => { action.Invoke(); };
        }
    }
    private void OnTriggerExit(Collider other) {
        //if (other.CompareTag("Player")) { other.GetComponent<Exploring>().waitingTriggers.Remove(this); }
        if (other.CompareTag("Player") && GameManager.Instance.state == GameManager.GameState.EXPLORATION) {
            other.GetComponent<Exploring>().interact = null;
        }
    }
}
