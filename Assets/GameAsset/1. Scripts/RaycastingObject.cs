// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public abstract class RaycastingObject : MonoBehaviour
{
    protected Transform playerTransform;
    public int cursorState = 2;

    private void OnEnable()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public abstract void Cast();
}
