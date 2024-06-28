using UnityEngine;

public class TeleportPlayer : MonoBehaviour {
    public Transform to;

    public void TeleporTo() {
        GameObject player = FindObjectOfType<Player>().gameObject;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = to.position;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
