using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusWaiter : MonoBehaviour
{
    Transform bus;
    Rigidbody busRb;
    Rigidbody rb;
    Transform doorTarget;
    Transform seatParent;
    Vector3 busLocalPosition;
    int seat = 0;
    Vector3 initPosition;

    private BusGuest guest;

    [SerializeField] Animator animator;

    private void Awake()
    {
        guest = GetComponent<BusGuest>();
    }
    enum States
    {
        Waiting,
        GettingOnDoor,
        GettingInSeat,
        In,
        GettingOut
    }
    States curState = States.Waiting;

    [SerializeField] float yOffset;

    void Start()
    {
        bus = GameObject.FindWithTag("Bus").transform;
        busRb = bus.GetComponentInChildren<Rigidbody>();
        rb = gameObject.GetComponent<Rigidbody>();
        doorTarget = bus.transform.Find("DoorTarget");
        seatParent = bus.transform.Find("Seats");
        initPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (guest.isGuesting) {
            if (animator != null) {
                if (rb.velocity.magnitude > 10) {
                    animator.SetInteger("state",4);
                } else{
                    animator.SetInteger("state",3);
                }
            }
        } else {
            switch (curState)
            {
                case States.Waiting:
                    if (Vector3.Distance(doorTarget.transform.position, transform.position) < 5 && busRb.velocity.magnitude <= 0.5f)
                        curState = States.GettingOnDoor;
                    if (transform.position != initPosition)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, initPosition, Time.deltaTime * 30);
                    }
                    if (animator != null)
                        animator.SetInteger("state",0);
                    break;
                case States.GettingOnDoor:
                    if (Vector3.Distance(doorTarget.transform.position, transform.position) > 0.2f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, doorTarget.transform.position, Time.deltaTime * 5);
                        if (Vector3.Distance(doorTarget.transform.position, transform.position) > 10)
                        {
                            curState = States.Waiting;
                        }
                    }
                    else
                    {
                        transform.position = doorTarget.transform.position;
                        busLocalPosition = doorTarget.transform.localPosition;
                        seat = Random.Range(0, seatParent.childCount);
                        curState = States.GettingInSeat;
                    }
                    if (animator != null)
                        animator.SetInteger("state",1);
                    break;
                case States.GettingInSeat:
                    if (Vector3.Distance(seatParent.GetChild(seat).transform.localPosition, busLocalPosition) > 0.2f)
                    {
                        busLocalPosition = Vector3.MoveTowards(busLocalPosition, seatParent.GetChild(seat).transform.localPosition, Time.deltaTime * 4);
                    }
                    else
                    {
                        busLocalPosition = seatParent.GetChild(seat).transform.localPosition;
                        curState = States.In;
                    }
                    transform.position = bus.transform.TransformPoint(busLocalPosition) + Vector3.up * yOffset;
                    if (animator != null)
                        animator.SetInteger("state",1);
                    break;
                case States.In:
                    if (animator != null)
                        animator.SetInteger("state",2);
                    break;
            }
        }

    }
}
