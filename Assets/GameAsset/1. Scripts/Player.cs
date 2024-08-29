using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    float h;
    float v;

    private new Rigidbody rigidbody;

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
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal"); // ���� �̵� �Է� ��
        v = Input.GetAxisRaw("Vertical");   // ���� �̵� �Է� ��
        Rotate();
        RaycastFromCamera(); // ����ĳ��Ʈ �˻�
    }

    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;    // ���콺 X�� �Է¿� ���� ���� ȸ�� ���� ����
        xRotation -= mouseY;    // ���콺 Y�� �Է¿� ���� ���� ȸ�� ���� ����

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // ���� ȸ�� ���� -90������ 90�� ���̷� ����

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // ī�޶��� ȸ���� ����
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             // �÷��̾� ĳ������ ȸ���� ����
    }

    private void FixedUpdate()
    {
        // �Է¿� ���� �̵� ���� ���� ���
        Vector3 moveVec = transform.forward * v + transform.right * h;

        // Rigidbody�� �̿��� �̵� ó��
        rigidbody.MovePosition(transform.position + moveVec.normalized * moveSpeed * Time.fixedDeltaTime);
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
                    Debug.Log(":DDD");
                    castObject.Cast();
                }
            }
        }
    }
}