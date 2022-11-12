using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeOilSpills : SteeringBehaviourBase, IEvade
{
    [SerializeField] private float evadeSpeed;
    [SerializeField] private float raycastLength;
    

    public override void OnObjectDetected(Powerup powerup)
    {
        if (powerup.CollisionType != CollisionTypes.OilSpill)
            return;
        

        EvadeTarget(powerup.transform);
    }

    public override void OnObjectStay(Powerup powerup)
    {
        if (powerup.CollisionType != CollisionTypes.OilSpill)
            return;
        

        EvadeTarget(powerup.transform);
    }

    public void EvadeTarget(Transform target)
    {
        Vector2 dirBetweenPoints = (target.position - transform.position).normalized;
        Vector3 dir = -Vector2.Perpendicular(dirBetweenPoints);
        Vector3 targetVelocity = dir * evadeSpeed;
        
        
        Force = targetVelocity * weight;
    }

    private void Update()
    {
        if (Force != Vector3.zero)
        {
            Force -= Force * Time.deltaTime;
        }
    }

}
