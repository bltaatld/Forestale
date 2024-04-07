using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public float destoryTime = 2f;
    public float rotationSpeed = 30f;

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void Start()
    {
        Destroy(gameObject, destoryTime);
    }
}
