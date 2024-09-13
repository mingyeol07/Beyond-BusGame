using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,transform.eulerAngles.z);
    }
}
