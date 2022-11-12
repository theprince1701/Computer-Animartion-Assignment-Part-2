using UnityEngine;

public class SeekPowerup : SteeringBehaviourBase, ISeeker
{
    [SerializeField] private float seekSpeed;


    public override void OnObjectDetected(Powerup powerup)
    {
        if (powerup.CollisionType != CollisionTypes.Powerup)
            return;
        
        SeekTarget(powerup.transform);
    }

    public void SeekTarget(Transform target)
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Vector3 currentVelocity = Owner.Rigidbody.velocity;
        Vector3 desiredVelocity = targetDirection * seekSpeed - currentVelocity;

        Force = desiredVelocity * weight * Owner.speedBoost;
    }
    
        private void Update()
    {
        if (Force != Vector3.zero)
        {
            Force -= Force * Time.deltaTime;
        }
    }
}
