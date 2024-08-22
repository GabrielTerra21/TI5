using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public String Name;
    public Portal[] doors = new Portal[4];
    public GameObject[] enemies;
    public GameObject treasure;
    public bool cleared = false;


    private void Start() {
    }
    
    public void EnterRoom() {
        if (cleared) {
            foreach(var data in doors) data.gameObject.SetActive(true);
        }
        else{
            if (enemies != null) {
                foreach (var data in enemies) data.SetActive(true); 
            }

            if (treasure != null) {
                treasure.SetActive(true);
            }
        }
    }

    public void ClearRoom() {
        cleared = true;
        foreach (var data in doors) {
            data.gameObject.SetActive(true);
        }
    }
}
