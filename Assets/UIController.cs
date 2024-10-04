using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Variables")]
    public Player player;

    [Header("Elements")]
    public Animator reticleController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //reticleController.SetInteger("state",player.cursorState);
    }
}
