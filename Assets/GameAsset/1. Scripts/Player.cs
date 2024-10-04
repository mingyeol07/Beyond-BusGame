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
    public int cursorState = 0;
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
            weapon.transform.rotation = handPosition.rotation;
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
                cursorState = castObject.cursorState;
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    castObject.Cast();
                }
            } else {
                cursorState = 0;
            }
        }
    }

    private void HandGuest()
    {
        if(guest != null)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                guest.GetComponent<Rigidbody>().useGravity = true;
                guest.GetComponent<Rigidbody>().isKinematic = false;
                guest.GetComponent<Rigidbody>().velocity = transform.up * gusetAwayForce + transform.forward * gusetAwayForce;
                guest = null;
            }
        }
        else if (weapon != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                handPosition.GetComponent<Animator>().SetTrigger("Attack");

                Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    if (hit.transform.gameObject.TryGetComponent(out BusGuest guest))
                    {
                        StartCoroutine(AttackSlowMotion());
                        guest.isGuesting = true;
                        guest.GetComponent<Rigidbody>().useGravity = true;
                        guest.GetComponent<Rigidbody>().isKinematic = false;
                        guest.GetComponent<Rigidbody>().velocity = transform.up * gusetAwayForce + transform.forward * gusetAwayForce;
                        guest.GetComponent<Collider>().enabled = false;
                    }
                }
            }
        }
    }

    private IEnumerator AttackSlowMotion()
    {
        float time = 0;

        while (time < 1)
        {
            time += Time.unscaledDeltaTime;

            Time.timeScale = time;
            Time.fixedDeltaTime = 0.02f * time;

            yield return null;
        }

        Time.timeScale = 1;
    }
}