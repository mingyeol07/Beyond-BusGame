using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CarMove : MonoBehaviour
{
    public Rigidbody rigid;

    private float currentAccel;
    private float currentTurnSpeed;

    [Header("Item")]
    public float smallUpSpeed = 2f;
    public float bigUpSpeed = 3f;

    [Header("Move")]
    public float forwardAccel = 6f;
    public float turnStrength = 45f;
    public float gravityForce = 10f;
    public float dragOnGround = 3f;
    private float speedInput, turnInput;

    [Header("Draft")]
    public bool isDrift = false;
    public float driftSpeed = 2f;
    public float driftTurn = 60f;

    [Header("Ground Check")]
    [SerializeField] private bool grounded;
    public LayerMask whatIsGround;
    public float groundRayLength = 0.5f;
    public Transform groundRayPoint;

    //[Header("Wheel Anim")]
    //public Transform leftFrontWheel;
    //public Transform rightFrontWheel;
    //public float maxWheelTurn = 25f;

    private void Start()
    {
        rigid.transform.parent = null;
        currentTurnSpeed = turnStrength;
    }
    private void Update()
    {
        // Input
        speedInput = 0f;
        if (Input.GetAxis("Vertical") != 0)
        {
            speedInput = Input.GetAxis("Vertical") * currentAccel * 1000f;
        }
        turnInput = Input.GetAxis("Horizontal");
        
        // Draft
        if (Input.GetKeyDown(KeyCode.LeftShift) && grounded)
        {
            isDrift = true;
            StartDrift();
        }

        // 드리프트 종료
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isDrift = false;
            currentAccel = forwardAccel;
            currentTurnSpeed = turnStrength;
        }

        // Rotation
        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnInput *
                currentTurnSpeed * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        // Animation
        //leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, leftFrontWheel.localRotation.eulerAngles.z);
        //rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, rightFrontWheel.localRotation.eulerAngles.z);

        // rigidbody의 포지션 따라가기
        transform.position = rigid.gameObject.transform.localPosition - new Vector3(0, 1.3f, 0);
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Drift();
    }

    private void GroundCheck()
    {
        grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            currentAccel = forwardAccel;
        }
        else if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, LayerMask.GetMask("NoneGround")))
        {
            grounded = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            currentAccel = 2f;
        }

        if (grounded)
        {
            rigid.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                rigid.AddForce(transform.forward * speedInput);
            }
        }
        else
        {
            rigid.drag = 0.1f;
            rigid.AddForce(Vector3.up * -gravityForce * 100f);
        }
    }

    private void Drift()
    {
        if (isDrift)
        {
            currentAccel = driftSpeed;
            currentTurnSpeed = driftTurn;
        }
    }

    private void StartDrift()
    {
        rigid.AddForce(Vector3.up * 250f, ForceMode.Impulse);

        if (turnInput > 0)
        {
            StartCoroutine(TurnDuringDrift(30f));
        }
        else if (turnInput < 0)
        {
            StartCoroutine(TurnDuringDrift(-30f));
        }
    }

    IEnumerator TurnDuringDrift(float turnAngle)
    {
        float turnTime = 0.25f;
        float timer = 0f;
        while (timer < turnTime)
        {
            transform.Rotate(0f, turnAngle * Time.deltaTime / turnTime, 0f);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
