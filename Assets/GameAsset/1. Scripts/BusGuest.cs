// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class BusGuest : MonoBehaviour
{
    [SerializeField]
    private BusGuestType busGuestType;

    // ������Ƽ => �б� ����
    public BusGuestType BusGuestType { get => busGuestType; }
}
