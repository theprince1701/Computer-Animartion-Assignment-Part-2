using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPowerup : SteeringBehaviourBase, ISeeker
{
    [SerializeField] private float lookDist;
    [SerializeField] private float seekSpeed;


    private void Update()
    {
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, transform.forward, out _hit, lookDist))
        {
            if (_hit.collider.TryGetComponent(out Powerup powerUp))
            {
                SeekTarget(_hit.collider.transform);
            }
        }
    }


    public void SeekTarget(Transform target)
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Vector3 currentVelocity = Owner.Rigidbody.velocity;
        Vector3 desiredVelocity = targetDirection * seekSpeed - currentVelocity;
        
        Force = desiredVelocity * weight;
    }
}
