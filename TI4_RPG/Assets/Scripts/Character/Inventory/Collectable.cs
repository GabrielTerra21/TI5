using System;
using UnityEngine;

public abstract class Collectable {
    [Space(10)]
    [Header("Colllectable object data")]
    public CollectableSO data;
    public int amount { get; private set; }
    public int maxAmount;


    public void Stack(int add) {
        amount += add;
        if (amount > maxAmount) {
            Debug.Log($"Max number of {data.Type} already in inventory, the exceeding {amount - maxAmount} will be discarted.");
            amount = maxAmount;
        }
    }

    // remove quantidade especificada da Stack e retorna se o restante é superior a 0 ou não
    public void Remove(int amount) {
        this.amount -= amount;
        if(amount <= 0) throw new Exception($"Collectable {data.Type} amount is currently {amount}. Negative values shouldn't occur");
    }
}
