using UnityEngine;

public class EngageSphere
{
    public float range;
    private Transform origin;
    public Collider[] enemies;


    private EngageSphere(float range, Transform origin){
        this.range = range;
        this.origin = origin;
    }

    public void CastRange(){
        enemies = Physics.OverlapSphere(origin.position, range);
    }
}
