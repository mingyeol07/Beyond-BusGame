// # Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusChair : RaycastingObject
{
    [SerializeField] private CarMove carMove;
    private bool isDriving;

    private void Update()
    {

    }

    private void LateUpdate()
    {
        if (isDriving)
        {
            playerTransform.position = transform.position;
        }
    }

    public override void Cast()
    {
        if(!isDriving)
        {
            carMove.enabled = true;
            playerTransform.GetComponent<Player>().isDriving = true;
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
            isDriving = true;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                carMove.enabled = false;
                playerTransform.GetComponent<Player>().isDriving = false;
                isDriving = false;
            }
        }
    }
}
