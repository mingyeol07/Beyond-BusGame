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
        if (player.cursorState == -1) {
            reticleController.gameObject.SetActive(false);
        } else {
            reticleController.gameObject.SetActive(true);
            reticleController.SetInteger("state",player.cursorState);
        }
    }
}
