using UnityEngine;

public class EngageSphere
{
    public float range;
    private Transform origin;
    public LayerMask layer;
    public Collider[] enemies;


    public EngageSphere(float range, Transform origin, LayerMask layer){
        this.range = range;
        this.origin = origin;
        this.layer = layer;

    }

    public void AggroRange(){
        enemies = Physics.OverlapSphere(origin.position, range, layer);
    }
}
