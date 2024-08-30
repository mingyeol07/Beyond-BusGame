using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : RaycastingObject
{
    public override void Cast()
    {
        if(playerTransform.GetComponent<Player>().guest == null)
        {
            playerTransform.GetComponent<Player>().weapon = this.gameObject;
        }
    }
}
