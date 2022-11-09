using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);

        collision.gameObject.TryGetComponent(out SteeringBehaviour car);

        if (car != null)
        {
            OnHit(car);
        }
    }


    protected abstract void OnHit(SteeringBehaviour car);
}
