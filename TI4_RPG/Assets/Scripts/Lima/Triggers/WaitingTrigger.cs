using UnityEngine;
using UnityEngine.Events;

public class WaitingTrigger : Trigger {
    [SerializeField] private GameObject key;
    [SerializeField] private UnityEvent onEndDialogue;
    private bool spent = false;
    protected override void Start() {
        if (oneTime) {
            action.AddListener(() => {
                action.RemoveAllListeners();
                action = null;
                key.SetActive(false);
                spent = true;
                //gameObject.SetActive(false);
            });
        }
    }
    
    protected virtual void OnTriggerEnter(Collider other) {
        //if (other.CompareTag("Player")) { other.GetComponent<Exploring>().waitingTriggers.Add(this); }
        if (other.CompareTag("Player") && GameManager.Instance.state == GameManager.GameState.EXPLORATION && action != null) {
            other.GetComponent<Exploring>().interact.AddListener(() => {
                action.Invoke();
                onEndDialogue?.Invoke();
            });
            if(key != null && !spent) key.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        //if (other.CompareTag("Player")) { other.GetComponent<Exploring>().waitingTriggers.Remove(this); }
        if (other.CompareTag("Player") && GameManager.Instance.state == GameManager.GameState.EXPLORATION) {
            other.GetComponent<Exploring>().interact.RemoveAllListeners();
            if(key != null && !spent) key.SetActive(false);
        }
    }
}
