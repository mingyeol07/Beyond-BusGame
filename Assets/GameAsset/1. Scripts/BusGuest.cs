// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class BusGuest : MonoBehaviour
{
    [SerializeField]
    private BusGuestType busGuestType;

    // 프로퍼티 => 읽기 전용
    public BusGuestType BusGuestType { get => busGuestType; }
}
