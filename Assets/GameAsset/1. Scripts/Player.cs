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
    public float raycastDistance = 100f; // ����ĳ��Ʈ �ִ� �Ÿ�

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // ���콺 Ŀ���� ȭ�� �ȿ��� ����
        Cursor.visible = false;                     // ���콺 Ŀ���� ������ �ʵ��� ����

        cam = Camera.main;                          // ���� ī�޶� �Ҵ�
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        xRotation = 0;
        yRotation = 0;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;    // ���콺 X�� �Է¿� ���� ���� ȸ�� ���� ����
        xRotation -= mouseY;    // ���콺 Y�� �Է¿� ���� ���� ȸ�� ���� ����

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        HandGuest();

        if (!isDriving)
        {
            h = Input.GetAxisRaw("Horizontal"); // ���� �̵� �Է� ��
            v = Input.GetAxisRaw("Vertical");   // ���� �̵� �Է� ��
            Rotate();
            RaycastFromCamera(); // ����ĳ��Ʈ �˻�
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
        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // ī�޶��� ȸ���� ����
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    void Rotate()
    {
       // ���� ȸ�� ���� -90������ 90�� ���̷� ����
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             // �÷��̾� ĳ������ ȸ���� ����
    }

    private void FixedUpdate()
    {
        if(!isDriving)
        {
            // �Է¿� ���� �̵� ���� ���� ���
            Vector3 moveVec = transform.forward * v + transform.right * h;

            // Rigidbody�� �̿��� �̵� ó��
            rigidbody.MovePosition(transform.position + moveVec.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void RaycastFromCamera()
    {
        // ī�޶��� �������� ������ ���̸� ���.
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // ����ĳ��Ʈ�� ��ü�� �ε������� �˻�
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