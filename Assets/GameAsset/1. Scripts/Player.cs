// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
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

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // ���콺 Ŀ���� ȭ�� �ȿ��� ����
        Cursor.visible = false;                     // ���콺 Ŀ���� ������ �ʵ��� ����

        cam = Camera.main;                          // ���� ī�޶� �Ҵ�
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal"); // ���� �̵� �Է� ��
        v = Input.GetAxisRaw("Vertical");   // ���� �̵� �Է� ��
        Rotate();
    }

    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;    // ���콺 X�� �Է¿� ���� ���� ȸ�� ���� ����
        xRotation -= mouseY;    // ���콺 Y�� �Է¿� ���� ���� ȸ�� ���� ����

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // ���� ȸ�� ���� -90������ 90�� ���̷� ����

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // ī�޶��� ȸ���� ����
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             // �÷��̾� ĳ������ ȸ���� ����
    }

    private void FixedUpdate()
    {
        // �Է¿� ���� �̵� ���� ���� ���
        Vector3 moveVec = transform.forward * v + transform.right * h;

        // �̵� ���͸� ����ȭ�Ͽ� �̵� �ӵ��� �ð� ������ ���� �� ���� ��ġ�� ����
        transform.position += moveVec.normalized * moveSpeed * Time.deltaTime;
    }
}
