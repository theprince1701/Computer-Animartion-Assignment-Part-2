using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionTypes
{
    Powerup,
    OilSpill
}

public class Powerup : MonoBehaviour
{
    [SerializeField] private CollisionTypes collisionTypes;

    public CollisionTypes CollisionType => collisionTypes;
    
    private void OnTriggerEnter(Collider collision)
    {
        collision.gameObject.TryGetComponent(out SteeringBehaviour car);

        if (car != null)
        {
            OnHit(car);
        }
    }

    protected virtual void OnHit(SteeringBehaviour car)
    {
        PathPlacer.Instance.RemovePowerUp(this);
        Destroy(gameObject);
    }
}
