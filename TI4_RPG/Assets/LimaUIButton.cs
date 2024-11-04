using UnityEngine;
using UnityEngine.EventSystems;

public class LimaUIButton : LimaUILib, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [SerializeField] private float distanceToMove;
    private bool moved, sized;
    
    public void OnPointerEnter(PointerEventData eventData) {
        Move();
    }

    public void OnPointerExit(PointerEventData eventData) {
        MoveBack();
    }

    public void OnPointerClick(PointerEventData eventData) {
        MoveBack();
    }

    
    private void Move() {
        if (!moved) {
            SmoothTranslateHorizontal(distanceToMove);
            moved = true;
        }
    }

    private void Increase() {
        if (!sized) {
            
        }
    }

    private void MoveBack() {
        if (moved) {
            SmoothTranslateHorizontal(-distanceToMove);
            moved = false;
        }
    }
}
