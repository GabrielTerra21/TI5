using UnityEngine;

public class WaitingTrigger : Trigger {
    [SerializeField] private GameObject key;
    protected override void Start() {
        if (oneTime) {
            action.AddListener(() => {
                action.RemoveAllListeners();
                action = null;
                key.SetActive(false);
                gameObject.SetActive(false);
            });
        }
    }
    
    protected virtual void OnTriggerEnter(Collider other) {
        //if (other.CompareTag("Player")) { other.GetComponent<Exploring>().waitingTriggers.Add(this); }
        if (other.CompareTag("Player") && GameManager.Instance.state == GameManager.GameState.EXPLORATION && action != null) {
            other.GetComponent<Exploring>().interact.AddListener( () => {if(action != null) action.Invoke(); });
            if(key != null) key.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        //if (other.CompareTag("Player")) { other.GetComponent<Exploring>().waitingTriggers.Remove(this); }
        if (other.CompareTag("Player") && GameManager.Instance.state == GameManager.GameState.EXPLORATION) {
            other.GetComponent<Exploring>().interact.RemoveAllListeners();
            if(key != null) key.SetActive(false);
        }
    }
}
