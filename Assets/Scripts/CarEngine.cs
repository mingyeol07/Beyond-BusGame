using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle = 5;
    public float maxMotorSpeed = 10;

    public Transform body;

    [SerializeField] private List<Transform> nodes;
    public int currentNode = 0;

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
        Vector3 relativeVector = nodes[currentNode].position - body.position;
        Quaternion rotation = Quaternion.LookRotation(relativeVector);
        Vector3 euler = rotation.eulerAngles;
        euler = new Vector3(0, euler.y, 0); // Keep only the y rotation for steering
        Quaternion newRotation = Quaternion.Euler(euler);
        body.rotation = Quaternion.Slerp(body.rotation, newRotation, Time.deltaTime * maxSteerAngle);
    }

    private void Drive()
    {
        body.Translate(Vector3.forward * maxMotorSpeed * Time.deltaTime);
    }

    private void CheckWayPoint()
    {
        if (Vector3.Distance(body.position, nodes[currentNode].position) < 1f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
}
