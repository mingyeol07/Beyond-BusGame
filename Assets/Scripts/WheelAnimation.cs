using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WheelAnimation : MonoBehaviour
{
    public WheelCollider target;

    private Vector3 wheelPosition;
    private Quaternion wheelRotation;

    void Update()
    {
        target.GetWorldPose(out wheelPosition, out wheelRotation);
        transform.position = wheelPosition;
        transform.rotation = wheelRotation;
    }
}
