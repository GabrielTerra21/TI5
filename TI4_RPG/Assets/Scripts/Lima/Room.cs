using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    public String ID;
    public Enemy[] enemies;
    public GameObject treasure;
    [SerializeField] private UnityEvent ev;

    
    // Adiciona ao evento de morte de cada inimigo presente
    // uma chamada do metodo que checa se todos os inimigos ja foram derrotados.
    public void Start() {
        EnterRoom();
    }
    
    // Checa se a sala ja foi vencida pelo player e desliga / liga
    // portas, inimigos e tesouros de acordo com o status da sala
    public void EnterRoom() {
        if (GameManager.Instance.CheckClearedRooms(ID)) {
            foreach (var data in enemies) data.gameObject.SetActive(false);
            return;
        }
        
        if (enemies != null)
            foreach (var data in enemies) data.gameObject.SetActive(true);
        if (treasure != null) treasure.SetActive(true);
        if(ev != null) ev.Invoke();
        
    }

    // Adiciona o ID da sala à lista de sala vencidas
    // ativa as portas da sala
    public void ClearRoom() {
        Debug.Log($"Room {ID} cleared");
        GameManager.Instance.AddClearedRoom(ID);
    }
    
    
    
    /*
    public void ApplyEffect(Effect effect)
    {
        foreach(Enemy enemy in enemies)
        {
            effect.DoStuff(enemy);
        }
    }
    */
}
