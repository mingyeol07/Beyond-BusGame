using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : RaycastingObject
{
    [SerializeField] private Transform target;
    private bool isWeaponing;

    private void LateUpdate()
    {
        if(!isWeaponing)
        {
            transform.position = target.position;
        }
    }

    public override void Cast()
    {
        if(playerTransform.GetComponent<Player>().guest == null)
        {
            isWeaponing = true;
            playerTransform.GetComponent<Player>().weapon = this.gameObject;
        }
    }
}
