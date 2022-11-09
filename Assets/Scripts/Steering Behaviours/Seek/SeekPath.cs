using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPath : SteeringBehaviourBase, ISeeker
{
    [SerializeField] private float pathFollowerSpeed;
    [SerializeField] private float seekSpeed;

    private Transform _target;

    private void Start()
    {
        PathFollower pathFollower = 
            new GameObject(gameObject.name + "_PATH FOLLOWER").AddComponent<PathFollower>();

        pathFollower.Init(pathFollowerSpeed, Owner);
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


        Force = desiredVelocity * weight;
        // Owner.SetSteeringForce(desiredVelocity * weight);
    }
}
