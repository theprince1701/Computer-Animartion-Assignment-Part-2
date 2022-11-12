using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeVehicles : SteeringBehaviourBase, IEvade
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

            if (dist <= fleeDistance && dist > 0)
            {
                EvadeTarget(fleeTargets[i]);
            }
        }
    }

    public void EvadeTarget(Transform target)
    {
        float t = 1f - Utility.Attenuate(target.position, Owner.Rigidbody.position, proximity);

        Vector3 a = -Owner.Rigidbody.velocity;
        Vector3 b = -Utility.Seek(target.position, Owner.Rigidbody,
            fleeSpeed);
        
        Force = Vector3.Lerp(a, b , t) * weight;
    }
}
