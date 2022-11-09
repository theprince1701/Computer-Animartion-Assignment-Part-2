using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class FleeVehicles : SteeringBehaviourBase, IFlee
{
    [SerializeField] private float fleeSpeed;
    [SerializeField] private float fleeDistance;
    [SerializeField] private float proximity;

    private List<Transform> fleeTargets = new List<Transform>();

    private void Start()
    {
        SteeringBehaviour[] vehicles = FindObjectsOfType<SteeringBehaviour>();

        for (int i = 0; i < vehicles.Length; i++)
        {
            fleeTargets.Add(vehicles[i].transform);
        }
    }


    private void Update()
    {
        for (int i = 0; i < fleeTargets.Count; i++)
        {
            float dist = (fleeTargets[i].position - transform.position).magnitude;

            if (dist <= fleeDistance)
            {
                FleeTarget(fleeTargets[i]);
            }
        }
    }

    public void FleeTarget(Transform target)
    {
        float t = Utility.Attenuate(target.position, Owner.Rigidbody.position, proximity);
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Vector3 currentVelocity = -Owner.Rigidbody.velocity;
        Vector3 desiredVelocity = targetDirection * fleeSpeed - currentVelocity;

        Force = Vector3.Lerp(-Owner.Rigidbody.velocity, -desiredVelocity, t) * weight;
    }
}
