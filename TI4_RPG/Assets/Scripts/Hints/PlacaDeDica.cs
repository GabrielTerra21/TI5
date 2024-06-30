using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlacaDeDica : MonoBehaviour
{
    public TMP_Text title, text;
    public Hint hint;
    void Start()
    {
        title.text = hint.title;
        text.text = hint.hintText;
    }
}
