using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrival : SteeringBehaviourBase, IArrival
{
    [SerializeField] private float slowRadius;
    [SerializeField] private float maxSpeed;
    
    
    public void ArriveAtTarget(Transform target)
    {
        Vector3 targetVelocity;

        float distance = (target.position - transform.position).magnitude;

        if (distance < slowRadius)
        {
            Vector3 targetDir = (target.position - transform.position).normalized;

            targetVelocity = targetDir * (maxSpeed * (distance / slowRadius));
        }
        else
        {
            Vector3 targetDir = (target.position - transform.position).normalized;

            targetVelocity = targetDir * maxSpeed;
        }
        Vector3 currentVelocity = Owner.Rigidbody.velocity;
        Vector3 steeringVector = targetVelocity - currentVelocity;


        Force = steeringVector * weight;
    }
}

