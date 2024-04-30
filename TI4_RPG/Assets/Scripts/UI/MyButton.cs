using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyButton : MonoBehaviour {
    public UnityEvent<string> buttonAction;
    

    public void Do(string menuName) {
        //buttonAction.Invoke();
    }
}
