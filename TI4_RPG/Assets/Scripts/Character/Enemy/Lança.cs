using UnityEngine;

public class Lança : Character
{

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Pause()
    {
        base.Pause();
    }

    public override void Unpause()
    {
        base.Unpause();
    }
    protected override void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        foreach (GameObject g in dependencies)
        {
            Destroy(g);
        }
        Debug.Log("Morri XD");
        OnDeath.Invoke();
        Destroy(gameObject);
    }
}
