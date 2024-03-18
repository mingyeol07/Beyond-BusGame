using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle = 45;
    public float maxMoterSpeed = 80;
    
    public Transform body;

    [SerializeField] private List<Transform> nodes;
    public int currentNode = 0;

    public WheelCollider wheelFR;
    public WheelCollider wheelFL;

    private void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        ApplySteer();
        Drive();
        CheckWayPoint();
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = body.transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        wheelFL.motorTorque = maxMoterSpeed;
        wheelFR.motorTorque = maxMoterSpeed;
    }

    private void CheckWayPoint()
    {
        if (Vector3.Distance(body.transform.position, nodes[currentNode].position) < 1f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else currentNode++;
        }
    }
}
