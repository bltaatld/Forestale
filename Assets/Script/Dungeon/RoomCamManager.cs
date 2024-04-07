using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;

public class RoomCamManager : MonoBehaviour
{
    public PolygonCollider2D confineCollider;
    private CinemachineConfiner2D confiner;
    private PixelPerfectCamera pixelPerfectCamera;

    private void Start()
    {
        confiner = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineConfiner2D>();
        pixelPerfectCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PixelPerfectCamera>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("Map").GetComponent<Map>() != null)
            {
                GameObject.FindGameObjectWithTag("Map").GetComponent<Map>().CheckRoom();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confiner.enabled = true;
            confiner.m_BoundingShape2D = confineCollider;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //pixelPerfectCamera.assetsPPU = 16;
            confiner.enabled = false;
            confiner.m_BoundingShape2D = null;
        }
    }
}
