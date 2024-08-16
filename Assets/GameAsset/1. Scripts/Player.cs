// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float    movementSpeed;
    private Vector3  inputVector;

    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.z = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        inputVector.Normalize();

        rigidbody.velocity = new Vector3(inputVector.x * movementSpeed,
            rigidbody.velocity.y, inputVector.z * movementSpeed);

        Debug.Log(inputVector);
    }
}
