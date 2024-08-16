// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class BusGuestInspection : MonoBehaviour
{
    [SerializeField]
    private float       range;              // ªÁ∞≈∏Æ
    [SerializeField]
    private LayerMask   guestLayerMask;

    private RaycastHit  raycastHit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, range, guestLayerMask))
            {
                ProcessGuestType(raycastHit);
            }
        }
    }

    private void ProcessGuestType(RaycastHit raycastHit)
    {
        BusGuestType busGuestType = raycastHit.collider.GetComponent<BusGuest>().BusGuestType;

        if (busGuestType == BusGuestType.NuisanceGuest)
        {
            Debug.Log("¿∏æ« πŒ∆Û º’¥‘¿Ã¥Ÿ ~!!!");
        }
        else if (busGuestType == BusGuestType.NormalGuest)
        {
            Debug.Log("ø¿ º’¥‘¿Ã¥Ÿ ~!!!");
        }
    }
}
