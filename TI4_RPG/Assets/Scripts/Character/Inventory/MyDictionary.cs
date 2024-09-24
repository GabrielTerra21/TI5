using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MyDictionary<T1, T2> {
    [SerializeField] private List<T1> keys = new List<T1>();
    [SerializeField] private List<T2> values = new List<T2>();
    
    public MyDictionary() {
        keys = new List<T1>();
        values = new List<T2>();
    }
    
    public MyDictionary(int size) {
        keys = new List<T1>(size);
        values = new List<T2>(size);
    }

    public void Add(T1 key, T2 value) {
        keys.Add(key);
        values.Add(value);
    }

    public T2 this[T1 key] {
        get {
            int index = keys.IndexOf(key);
            Debug.Log($"{key}, {index}");
            return values[index];
        }
        set {
            int index = keys.IndexOf(key);
            values[index] = value;
        }
    }
}
