using System;
using System.Collections.Generic;

[Serializable]
public class MyDictionary<T1, T2> {
    public List<T1> keys;
    public List<T2> values;
    
    public MyDictionary() {
        keys = new List<T1>();
        values = new List<T2>();
    }
    
    public MyDictionary(int size) {
        keys = new List<T1>(size);
        values = new List<T2>(size);
    }

    public void Add(T1 key, T2 value) {
        
    }
}
