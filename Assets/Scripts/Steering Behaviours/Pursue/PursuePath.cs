using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuePath : SteeringBehaviourBase, IPursue
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
        PursueTarget(_target);
    }
    

    public void PursueTarget(Transform target)
    {
        Vector3 velocity = Owner.Rigidbody.velocity;
        
        Vector3 targetDirection = (target.position + velocity - transform.position).normalized;
        Vector3 currentVelocity = velocity;
        Vector3 desiredVelocity = targetDirection * seekSpeed - currentVelocity;
        
        Force = desiredVelocity * weight;
    }
}
