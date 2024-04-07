using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician_Fire : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnRadius = 5f;
    public string targetTag;

    private void Start()
    {
       Destroy(gameObject, 0.5f); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Vector3 spawnPosition = other.transform.position;
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
