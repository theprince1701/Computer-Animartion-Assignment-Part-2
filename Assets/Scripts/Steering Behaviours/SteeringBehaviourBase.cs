using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LookTypes
{
    Raycast,
    Distance,
    Both
}

public class SteeringBehaviourBase : MonoBehaviour
{
    [SerializeField, Range(0, 1)] protected float weight;


    protected SteeringBehaviour Owner;
    
    public Vector3 Force { get; set; }

    public virtual void Init(SteeringBehaviour owner)
    {
        Owner = owner;
    }
    
    public virtual void OnObjectDetected(Powerup powerup) {  }
    public virtual void OnObjectStay(Powerup powerup) {  }

}
