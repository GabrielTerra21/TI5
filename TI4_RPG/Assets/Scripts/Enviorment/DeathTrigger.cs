using UnityEngine;

public class DeathTrigger : Trigger
{
    private void OnDestroy()
    {
        action.Invoke();
    }
}
