using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Da_Move : MonoBehaviour
{

    public float m_speed;
    public Rigidbody m_Rigid;
    public float m_turnspeed;


    void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MyMove();
        MyRotate();
    }

    private void MyMove()
    {
        float m_movement = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * m_movement * m_speed * Time.deltaTime;
        m_Rigid.MovePosition(m_Rigid.position + movement);
    }

    private void MyRotate()
    {
        float turnValue = Input.GetAxis("Horizontal");
        float turn = turnValue * m_turnspeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0);
        m_Rigid.MoveRotation(m_Rigid.rotation * turnRotation);
    }


}