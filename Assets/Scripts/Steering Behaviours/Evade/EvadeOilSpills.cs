using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeOilSpills : SteeringBehaviourBase, IEvade
{
    [SerializeField] private float evadeSpeed;

    public override void OnObjectDetected(Powerup powerup)
    {
        if (powerup.CollisionType != CollisionTypes.OilSpill)
            return;
        
        EvadeTarget(powerup.transform);
    }


    public void EvadeTarget(Transform target)
    {
        Vector2 dirBetweenPoints = (target.position - transform.position).normalized;
        Vector3 dir = Vector2.Perpendicular(dirBetweenPoints);
        Vector3 targetVelocity = (dir * evadeSpeed) + Owner.Rigidbody.velocity;
        
        Force = targetVelocity * weight;
        Invoke(nameof(ResetForce), 1.0f);
    }

    private void ResetForce()
    {
        Force = Vector3.zero;
    }
}
