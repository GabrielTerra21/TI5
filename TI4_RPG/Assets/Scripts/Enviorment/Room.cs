using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public String ID;
    public Enemy[] enemies;
    public GameObject treasure;

    
    // Adiciona ao evento de morte de cada inimigo presente
    // uma chamada do metodo que checa se todos os inimigos ja foram derrotados.
    public void Start() {
        EnterRoom();
    }
    
    // Checa se a sala ja foi vencida pelo player e desliga / liga
    // portas, inimigos e tesouros de acordo com o status da sala
    public void EnterRoom() {
        if (GameManager.Instance.CheckClearedRooms(ID)) {
            Debug.Log($"Room {ID} already cleared");
        }
        else{
            if (enemies != null) {
                Debug.Log("Spawning Enemies");
                foreach (var data in enemies) {
                    data.gameObject.SetActive(true);
                    //data.OnDeath.AddListener(CheckIfEmpty);
                }
            }
            if (treasure != null) {
                treasure.SetActive(true);
            }
        }
    }

    // Checa se todos os inimigos da sala foram mortos
    // chama metodo de vencer sala se for verdade.
    /*
    public void CheckIfEmpty() {
        foreach (var data in enemies) {
            if(data != null) return;
        }
        ClearRoom();
    }
    */

    // Adiciona o ID da sala à lista de sala vencidas
    // ativa as portas da sala
    public void ClearRoom() {
        Debug.Log($"Room {ID} cleared");
        GameManager.Instance.AddClearedRoom(ID);
    }
}
