using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPath : SteeringBehaviourBase, ISeeker
{
    [SerializeField] private float seekSpeed;

    private Transform _target;

    private void Start()
    {
        SpeedControlledPathFollower pathFollower = 
            new GameObject(gameObject.name + "_PATH FOLLOWER").AddComponent<SpeedControlledPathFollower>();
        
        pathFollower.Init(Owner);
        _target = pathFollower.transform;
    }

    private void Update()
    {
        SeekTarget(_target);

    }

    public void SeekTarget(Transform target)
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Vector3 currentVelocity = Owner.Rigidbody.velocity;
        Vector3 desiredVelocity = targetDirection * seekSpeed - currentVelocity;


        Force = desiredVelocity * weight * Owner.speedBoost;
        
        transform.rotation = Quaternion.LookRotation(targetDirection);

    }
}
