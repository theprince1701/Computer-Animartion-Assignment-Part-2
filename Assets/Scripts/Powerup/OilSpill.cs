using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpill : Powerup
{
    protected override void OnHit(SteeringBehaviour car)
    {
        PathPlacer.Instance.RemovePowerUp(this);
    }
}
