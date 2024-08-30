using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    float h;
    float v;
    public bool isDriving;

    private new Rigidbody rigidbody;

    [SerializeField] private Transform handPosition;
    [SerializeField] public GameObject guest;
    [SerializeField] public GameObject weapon;

    [SerializeField] private float gusetAwayForce;

    [Header("Rotate")]
    public float mouseSpeed;
    private float yRotation;
    private float xRotation;
    private Camera cam;

    [Header("Raycast")]
    public float raycastDistance = 100f; // 레이캐스트 최대 거리

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서를 화면 안에서 고정
        Cursor.visible = false;                     // 마우스 커서를 보이지 않도록 설정

        cam = Camera.main;                          // 메인 카메라를 할당
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        xRotation = 0;
        yRotation = 0;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;    // 마우스 X축 입력에 따라 수평 회전 값을 조정
        xRotation -= mouseY;    // 마우스 Y축 입력에 따라 수직 회전 값을 조정

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        HandGuest();

        if (!isDriving)
        {
            h = Input.GetAxisRaw("Horizontal"); // 수평 이동 입력 값
            v = Input.GetAxisRaw("Vertical");   // 수직 이동 입력 값
            Rotate();
            RaycastFromCamera(); // 레이캐스트 검사
        }
    }

    private void LateUpdate()
    {
        CamRotate();

        if(guest != null)
        {
            guest.transform.position = handPosition.position;
        }
        else if(weapon != null)
        {
            weapon.transform.position = handPosition.position;
            weapon.transform.rotation = transform.rotation;
        }
    }

    private void CamRotate()
    {
        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // 카메라의 회전을 조절
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    void Rotate()
    {
       // 수직 회전 값을 -90도에서 90도 사이로 제한
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             // 플레이어 캐릭터의 회전을 조절
    }

    private void FixedUpdate()
    {
        if(!isDriving)
        {
            // 입력에 따라 이동 방향 벡터 계산
            Vector3 moveVec = transform.forward * v + transform.right * h;

            // Rigidbody를 이용해 이동 처리
            rigidbody.MovePosition(transform.position + moveVec.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void RaycastFromCamera()
    {
        // 카메라의 시점에서 앞으로 레이를 쏜다.
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // 레이캐스트가 물체에 부딪혔는지 검사
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            if (hit.transform.gameObject.TryGetComponent(out RaycastingObject castObject))
            {
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    castObject.Cast();
                }
            }
        }
    }

    private void HandGuest()
    {
        if(guest != null)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                guest.GetComponent<Rigidbody>().velocity = transform.up * gusetAwayForce + transform.forward * gusetAwayForce;
                guest = null;
            }
        }
    }
}