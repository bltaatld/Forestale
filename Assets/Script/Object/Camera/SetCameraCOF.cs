using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraCOF : MonoBehaviour
{
    public Animator camAnim;
    public int camNum;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            camAnim.SetInteger("CurrnetCam", camNum);
        }
    }
}
