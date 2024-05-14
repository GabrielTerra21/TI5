using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void ChangeScene(string s){
        SceneManager.LoadScene(s);
    }
}
