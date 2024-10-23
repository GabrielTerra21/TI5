using UnityEngine;

public class DoorManager : MonoBehaviour {
    private Portal[] doors;


    private void Start() {
        FindDoors();
    }

    private void FindDoors() {
        doors = FindObjectsByType<Portal>(FindObjectsInactive.Include, sortMode: FindObjectsSortMode.None);
        foreach (var data in doors) {
            Debug.Log(data.name);
        }
    }

    public void SetDoorsActive(string roomID) {
        foreach (var data in doors) {
            data.gameObject.SetActive(data.roomID == roomID);
        }
    }
}
