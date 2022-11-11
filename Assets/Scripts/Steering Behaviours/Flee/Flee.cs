using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehaviourBase, IFlee
{
    [SerializeField] private float fleeSpeed;
    [SerializeField] private CollisionTypes fleeType;


    public override void OnObjectDetected(Powerup powerup)
    {
        if (powerup.CollisionType != fleeType)
            return;
        
        FleeTarget(powerup.transform);
    }

    public void FleeTarget(Transform target)
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Vector3 currentVelocity = -Owner.Rigidbody.velocity;
        Vector3 desiredVelocity = targetDirection * fleeSpeed - currentVelocity;

        Force = desiredVelocity * weight;
    }
}
