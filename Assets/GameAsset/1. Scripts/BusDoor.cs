// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class BusDoor : RaycastingObject
{
    private bool isInBus;

    [SerializeField] private Transform inTransform;
    [SerializeField] private Transform outTransform;

    public override void Cast()
    {
        Debug.Log("Move");

        if (!isInBus)
        {
            playerTransform.position = inTransform.position;
        }
        else
        {
            playerTransform.position = outTransform.position;
        }

        isInBus = !isInBus;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BusGuest guest))
        {
            
        }
    }
}
