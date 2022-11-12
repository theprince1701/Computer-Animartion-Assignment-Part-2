using System;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour
{
    [SerializeField] private SteeringBehaviour car;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Powerup powerup))
        {
            car.onObstacleDetected?.Invoke(powerup);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Powerup powerup))
        {
            car.onObstacleStay?.Invoke(powerup);
        }
    }
}
