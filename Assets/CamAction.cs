using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DynamicCam();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DynamicCamEnd();
            Destroy(gameObject);
        }
    }


    public void DynamicCam()
    {
        Debug.Log("asdasd");
        CameraShake.instance.Shakecamera(3f, 50f);
    }

    public void DynamicCamEnd()
    {
        CameraShake.instance.isNeedInstanEnd = true;
    }
}
