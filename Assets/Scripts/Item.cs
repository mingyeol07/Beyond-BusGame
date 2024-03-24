using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ItemManager.instance.GetItem();
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Ai"))
        {
            gameObject.SetActive(false); ;
        }
    }

    private void OnDisable()
    {
        Invoke("Set", 3f);
    }

    private void Set()
    {
        gameObject.SetActive(true);
    }
}
