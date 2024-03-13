using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    public Transform node;
    private List<Transform> nodes;
    private int currentNode = 0;

    public float maxSteerAngle = 45;
    public WheelCollider wheelFR;
    public WheelCollider wheelFL;

    private void Start()
    {
        Transform[] nodeInPath = GetComponentsInChildren<Transform>();
        nodes = new List<Transform> ();

        for (int i = 0; i < nodeInPath.Length; i++)
        {
            if (nodeInPath[i] != node.transform)
            {
                nodes.Add(nodeInPath[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        ApplySteer();
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }
}
