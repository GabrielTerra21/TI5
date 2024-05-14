using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : EventTrigger
{
    public RectTransform rect;

    private void Awake(){
        rect = GetComponent<RectTransform>();
    }

    public override void OnPointerEnter(){
        
    }
}
