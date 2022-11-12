using System;
using UnityEngine;
using UnityEngine.Events;

public enum PhysicsUpdateTypes
{
    Update,
    FixedUpdate,
    LateUpdate
}

[RequireComponent(typeof(Rigidbody))]
public class SteeringBehaviour : MonoBehaviour
{
    public PhysicsUpdateTypes updateType;
    
    private SteeringBehaviourBase[] _modules;
    
    public float speedBoost { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public UnityEvent<Powerup> onObstacleDetected;
    public UnityEvent<Powerup> onObstacleStay;

    private void Awake()
    {
        speedBoost = 1f;
        Rigidbody = GetComponent<Rigidbody>();
        _modules = GetComponents<SteeringBehaviourBase>();

        for (int i = 0; i < _modules.Length; i++)
        {
            _modules[i].Init(this);
            onObstacleDetected.AddListener(_modules[i].OnObjectDetected);
            onObstacleStay.AddListener(_modules[i].OnObjectStay);
        }
    }

    private void Update()
    {
        if (updateType == PhysicsUpdateTypes.Update)
        {
            UpdatePhysics();
        }

        if (speedBoost > 1.0f)
        {
            speedBoost -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(updateType == PhysicsUpdateTypes.FixedUpdate)
        {
            UpdatePhysics();
        }
    }

    private void LateUpdate()
    {
        if (updateType == PhysicsUpdateTypes.LateUpdate)
        {
            UpdatePhysics();
        }
    }
    
    private void UpdatePhysics()
    {
        Vector3 force = Vector3.zero;

        foreach (SteeringBehaviourBase module in _modules)
        {
            force += module.Force;
        }

        Rigidbody.AddForce(force);
    }

    public void Spin(float spinForce)
    {
        Vector3 spinDir = transform.right;
        
        Rigidbody.AddTorque(spinDir * spinForce);
    }

    public void SpeedBoost(float speedBoost)
    {
        this.speedBoost = speedBoost;
    }
}
