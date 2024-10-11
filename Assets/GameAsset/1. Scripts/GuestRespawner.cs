using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestRespawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnTransform;
    [SerializeField] private GameObject[] guestPrefabs;

    private void Awake()
    {
        SpawnGuests();
    }

    private void OnTriggerEnter(Collider collision)
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
            int random = Random.Range(0, guestPrefabs.Length);

            Instantiate(guestPrefabs[random], spawnTransform[i].position, Quaternion.identity);
        }
    }
}
