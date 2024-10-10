using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestRespawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnTransform;
    [SerializeField] private GameObject guestPrefab;

    private void Start()
    {
        SpawnGuests();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SpawnGuests();
        }
    }

    private void SpawnGuests()
    {
        for(int i =0; i < spawnTransform.Length; i++)
        {
            Instantiate(guestPrefab, spawnTransform[i].position, Quaternion.identity);
        }
    }
}
