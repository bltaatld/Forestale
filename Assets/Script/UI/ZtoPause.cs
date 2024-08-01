using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZtoPause : MonoBehaviour
{
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}
