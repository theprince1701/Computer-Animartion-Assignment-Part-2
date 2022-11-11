using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static Vector3 Seek(Vector3 target, Rigidbody current, float speed)
    {
        Vector3 targetDirection = (target - current.position).normalized;
        Vector3 currentVelocity = current.velocity;
        Vector3 desiredVelocity = targetDirection * speed - currentVelocity;
        return desiredVelocity;
    }

    // Approaches 0 as current approaches target. Returns 1 if current is length or more units away from target. 
    public static float Attenuate(Vector3 target, Vector3 current, float length)
    {
        return Mathf.Clamp01((target - current).magnitude / length);
    }
    
    public static Vector3 GetCurvePosition(Vector3 s, Vector3 e, Vector3 st, Vector3 et, float t)
    {
        return (((-s + 3*(st-et) + e)* t + (3*(s+et) - 6*st))* t + 3*(st-s))* t + s;
    }
}
