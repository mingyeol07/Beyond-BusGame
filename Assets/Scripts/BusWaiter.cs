using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField] private GameObject[] animations;
    [SerializeField] private GameObject txt_bubble;


    private void SetBubble()
    {
        txt_bubble.SetActive(true);

        TMP_Text txt = txt_bubble.GetComponentInChildren<TMP_Text>();

        int ran = Random.Range(0, 3);

        if(ran == 0)
        {
            txt.text = "요금 조금만 깎아주세요~";
        }
        else if (ran == 1)
        {
            txt.text = "버스가 왜이리 더럽습니까?";
        }
        else if (ran == 2)
        {
            txt.text = "빨리빨리좀 다니세요";
        }
    }
    public void SetAnimationPrefab(int index)
    {
        for(int i =0; i < animations.Length; i++)
        {
            animations[i].SetActive(false);
        }

        animations[index].SetActive(true);
    }
    private void Awake()
    {
        guest = GetComponent<BusGuest>();
        bus = GameObject.FindWithTag("Bus").transform.GetChild(0);
        busRb = GameObject.Find("MoveCollider").GetComponent<Rigidbody>();
        rb = gameObject.GetComponent<Rigidbody>();
        doorTarget = bus.transform.Find("DoorTarget");
        seatParent = bus.transform.Find("Seats");
        initPosition = transform.position;
        SetAnimationPrefab(0);
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
                    {
                        SetAnimationPrefab(1);
                        curState = States.GettingOnDoor;
                    }

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
                        if(guest.BusGuestType == BusGuestType.NuisanceGuest)
                        {
                            SetAnimationPrefab(4);
                            SetBubble();
                        }
                        else
                        {
                            SetAnimationPrefab(2);
                        }

                        GameManager.Instance.busGuests.Add(this.guest);
                        StartCoroutine(Disappear());
                        curState = States.In;
                    }
                    transform.position = bus.transform.TransformPoint(busLocalPosition) + Vector3.up * yOffset;
                    if (animator != null)
                        animator.SetInteger("state",1);
                    break;
                case States.In:
                    transform.position = bus.transform.TransformPoint(busLocalPosition) + Vector3.up * yOffset;
                    if (animator != null)
                        animator.SetInteger("state",2);
                    break;
            }
        }

        IEnumerator Disappear()
        {
            int ran = Random.Range(15, 25);
            yield return new WaitForSeconds(ran);
            if (GameManager.Instance.busGuests.Contains(this.guest) )
            {
                GameManager.Instance.busGuests.Remove(this.guest);
                Destroy(this.gameObject);
            }
        }
    }
}
