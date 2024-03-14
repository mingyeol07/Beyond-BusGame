using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCar : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheels = new WheelCollider[4];
    [SerializeField] private GameObject[] wheelMeshes = new GameObject[4];
    [SerializeField] private float motorTorque = 300f;
    [SerializeField] private float brakePower = 1000f;
    [SerializeField] private float steeringMaxAngle = 45f;

    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.2f, 0);
    }

    private void FixedUpdate()
    {
        Move();
        SteerCar();
    }

    private void Move()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].motorTorque = motorTorque * Input.GetAxis("Vertical");
        }
    }

    private void SteerCar()
    {
        for (int i = 0; i < 2; i++)
        {
            wheels[i].steerAngle = steeringMaxAngle * Input.GetAxis("Horizontal");
        }
    }

    
}
