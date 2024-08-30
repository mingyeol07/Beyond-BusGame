// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class BusGuest : RaycastingObject
{
    [SerializeField]
    private BusGuestType busGuestType;

    // 프로퍼티 => 읽기 전용
    public BusGuestType BusGuestType { get => busGuestType; }

    public override void Cast()
    {
        playerTransform.GetComponent<Player>().guest = this.gameObject;
    }
}
