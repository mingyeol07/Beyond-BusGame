using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float moveSmoothness;
    [SerializeField] private float rotSmoothness;

    [SerializeField] private Vector3 moveOffset;
    [SerializeField] private Vector3 rotOffset;

    [SerializeField] private Transform carTarget;

    private bool IsFirstPositionToggle;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            IsFirstPositionToggle = !IsFirstPositionToggle;
        }
    }

    private void FixedUpdate()
    {
        if(IsFirstPositionToggle)
        {

        }
        else
        {
            HandleMovement();
            HandleRotation();
        }
    }

    void HandleMovement()
    {
        Vector3 targetPos = new Vector3();
        targetPos = carTarget.TransformPoint(moveOffset);

        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.deltaTime);
    }

    void HandleRotation()
    {
        var direction = carTarget.position - transform.position;
        var rotation = new Quaternion();
        rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime);
    }
}
