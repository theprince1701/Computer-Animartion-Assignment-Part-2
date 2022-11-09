using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeOilSpills : SteeringBehaviourBase, IEvade
{
    [SerializeField] private float evadeSpeed;
    [SerializeField] private float evadeProximity;
    [SerializeField] private float lookDist;

    private void Update()
    {
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, transform.forward, out _hit, lookDist))
        {
            if (_hit.collider.TryGetComponent(out OilSpill oilSpill))
            {
                EvadeTarget(_hit.collider.transform);
            }
        }
    }

    public void EvadeTarget(Transform target)
    {
        float t = 1.0f - Utility.Attenuate(target.position, Owner.Rigidbody.position, evadeProximity);
        
        Vector3 targetDirection = (target.position + Owner.Rigidbody.velocity - transform.position).normalized;
        Vector3 currentVelocity = Owner.Rigidbody.velocity;
        Vector3 desiredVelocity = targetDirection * evadeSpeed - currentVelocity;

        Force = Vector3.Lerp(currentVelocity, desiredVelocity, t);
    }
}
