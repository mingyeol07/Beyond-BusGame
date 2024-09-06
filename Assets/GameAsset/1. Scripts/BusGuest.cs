// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class BusGuest : RaycastingObject
{
    [SerializeField]
    private BusGuestType busGuestType;

    // ������Ƽ => �б� ����
    public BusGuestType BusGuestType { get => busGuestType; }
    public bool isGuesting;
    public override void Cast()
    {
        if(playerTransform.GetComponent<Player>().weapon == null)
        { 
            playerTransform.GetComponent<Player>().guest = this.gameObject;

        }
        isGuesting = true;
    }
}
