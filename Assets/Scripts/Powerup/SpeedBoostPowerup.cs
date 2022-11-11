using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostPowerup : Powerup
{
    [SerializeField] private float speedBoost;
    
    protected override void OnHit(SteeringBehaviour car)
    {
  //      car.SpeedBoost(speedBoost);
        PathPlacer.Instance.RemovePowerUp(this);
    }
}
