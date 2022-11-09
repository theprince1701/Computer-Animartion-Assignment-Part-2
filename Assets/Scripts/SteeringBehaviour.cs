using System;
using UnityEngine;

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
    
    public Rigidbody Rigidbody { get; private set; }
    
    private SteeringBehaviourBase[] _modules;

    private float _speedBoost = 1.0f;
    
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _modules = GetComponents<SteeringBehaviourBase>();

        for (int i = 0; i < _modules.Length; i++)
        {
            _modules[i].Init(this);
        }
    }

    private void Update()
    {
        if (updateType == PhysicsUpdateTypes.Update)
        {
            UpdatePhysics();
        }

        if (_speedBoost > 1.0f)
        {
            _speedBoost -= Time.deltaTime;
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
        
        Rigidbody.AddForce(force * _speedBoost);
    }

    public void Spin(float spinForce)
    {
        Vector3 spinDir = transform.right;
        
        Rigidbody.AddTorque(spinDir * spinForce);
    }

    public void SpeedBoost(float speedBoost)
    {
        Debug.Log(speedBoost);
        _speedBoost = speedBoost;
    }
}
