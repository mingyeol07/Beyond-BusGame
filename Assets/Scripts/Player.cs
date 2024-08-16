namespace Mingyeoul
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    private float speed = 6;
    private float hor;
    private float ver;
    private Rigidbody rigid;

    public class BusGuestInspection : MonoBehaviour
    {
        [SerializeField]
        private float range;      // ��Ÿ�
        [SerializeField]
        private LayerMask guestLayerMask;

        private RaycastHit raycastHit;

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
                Debug.Log("���� ���� �մ��̴� ~!!!");
            }
            else if (busGuestType == BusGuestType.NormalGuest)
            {
                Debug.Log("�� �մ��̴� ~!!!");
            }
        }
    }

}
    private void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector3(hor, rigid.velocity.y, ver).normalized * speed;
    }
}
