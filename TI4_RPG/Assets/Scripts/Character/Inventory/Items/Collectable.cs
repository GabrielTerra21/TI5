using System;
using UnityEngine;

public class Collectable  : MonoBehaviour{
    [Space(10)]
    [Header("Colllectable object data")]
    public CollectableSO data;
    public int amount { get; private set; }


    public void Stack(int add) {
        amount += add;
        if (amount > data.stackAmount) {
            Debug.Log($"Max number of {data.type} already in inventory, the exceeding {amount - data.stackAmount} will be discarted.");
            amount = data.stackAmount;
        }
    }

    // remove quantidade especificada da Stack e retorna se o restante é superior a 0 ou não
    public void Remove(int amount) {
        this.amount -= amount;
        if(amount <= 0) throw new Exception($"Collectable {data.type} amount is currently {amount}. Negative values shouldn't occur");
    }
}
