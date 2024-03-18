using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCar : MonoBehaviour
{
    public Rigidbody rigid;

    private float currentAccel;
    private float currentTurnSpeed;

    [Header("Move")]
    public float forwardAccel = 6f;
    public float reverseAccel = 4f;
    public float turnStrength = 45f;
    public float gravityForce = 10f;
    public float dragOnGround = 3f;
    private float speedInput, turnInput;

    [Header("Draft")]
    public bool isDrift = false;
    public float driftSpeed = 4f;
    public float driftTurn = 90f;

    [Header("Ground Check")]
    private bool grounded;
    public LayerMask whatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;

    [Header("Wheel Anim")]
    public Transform leftFrontWheel;
    public Transform rightFrontWheel;
    public float maxWheelTurn= 25f;

    [Header("Trail Particle")]
    public ParticleSystem[] dustTrail;
    public float maxEmission = 25f;
    private float emissionRate;

    private void Start()
    {
        rigid.transform.parent = null;
    }
    private void Update()
    {
        // Input
        speedInput = 0f;
        if(Input.GetAxis("Vertical") > 0)
                {
            speedInput = Input.GetAxis("Vertical") * currentAccel * 1000f;

        } else if(Input.GetAxis("Vertical") < 0)
                {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }
        turnInput = Input.GetAxis("Horizontal");

        
        // Draft
        if (Input.GetKeyDown(KeyCode.LeftShift) && grounded)
        {
            isDrift = true;
            rigid.AddForce(Vector3.up * 50f, ForceMode.Impulse);
            StartDrift();
        }

        // 드리프트 종료
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isDrift = false;
            EndDrift();
        }

        // Rotation
        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnInput *
                currentTurnSpeed * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        // Animation
        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, rightFrontWheel.localRotation.eulerAngles.z);

        // rigidbody의 포지션 따라가기
        transform.position = rigid.gameObject.transform.localPosition - new Vector3(0, 0.5f, 0) ;
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Trail();
        Draft();
    }

    private void GroundCheck()
    {
        // 땅에 붙어있는지 확인
        grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

    }

    private void Draft()
    {
        if(isDrift)
        {
            currentAccel = driftSpeed;
            currentTurnSpeed = driftTurn;
        }
        else
        {
            currentAccel = forwardAccel;
            currentTurnSpeed = turnStrength;
        }
    }

    private void Trail()
    {
        emissionRate = 0;

        if (grounded)
        {
            rigid.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                rigid.AddForce(transform.forward * speedInput);
                emissionRate = maxEmission;
            }
        }
        else
        {
            rigid.drag = 0.1f;
            rigid.AddForce(Vector3.up * -gravityForce * 100f);
        }

        foreach (ParticleSystem part in dustTrail)
        {
            var emissionModule = part.emission;
            emissionModule.rateOverTime = emissionRate;
        }
    }

    // 드리프트 시작 시 호출될 함수
    void StartDrift()
    {
        rigid.AddForce(Vector3.up * 250f, ForceMode.Impulse);

        // 드리프트 시작 시 이동 방향으로 잠깐 회전하는 연출 추가
        if (speedInput > 0) // 전진 중일 때
        {
            StartCoroutine(TurnDuringDrift(turnInput > 0 ? 15f : -15f)); // 방향 입력에 따라 회전
        }
        else if (speedInput < 0) // 후진 중일 때
        {
            StartCoroutine(TurnDuringDrift(turnInput > 0 ? -15f : 15f)); // 후진 시 방향 반전
        }
    }

    // 드리프트 종료 시 호출될 함수
    void EndDrift()
    {
        // 드리프트 종료 시 필요한 로직 추가 (현재는 비어 있음)
    }

    // 드리프트 중 회전 연출을 위한 코루틴
    IEnumerator TurnDuringDrift(float turnAngle)
    {
        float turnTime = 0.5f; // 회전이 지속될 시간 (초)
        float timer = 0f;
        while (timer < turnTime)
        {
            // 이동 방향쪽으로 잠깐 회전
            transform.Rotate(0f, turnAngle * Time.deltaTime / turnTime, 0f);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
