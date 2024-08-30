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

    public override void Cast()
    {
        playerTransform.GetComponent<Player>().guest = this.gameObject;
    }
}
