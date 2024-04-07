using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public Vector3 teleportPosition;
    public Vector3 bossTeleportPosition;
    public float delayTime = 10f;
    public FadeScreen m_fade;
    private GameObject m_transportTarget;
    private MapGenerator m_generator;

    void Start()
    {
        m_fade = FadeScreen.GetInstance();
        m_generator = GameObject.Find("DungeonManager").GetComponent<MapGenerator>();
        m_transportTarget = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_fade.FadeInOut(FadeScreen.FadeInTime, FadeScreen.FadeOutTime, FadeScreen.FadeDelayTime, TeleportPosition);
        }
    }

    public void TeleportPosition()
    {
        //Boss Teleport
        if (m_generator.currentFloor > 2)
        {
            Debug.Log("Boss Room Teleported");
            m_transportTarget.transform.position = bossTeleportPosition;
            m_transportTarget.GetComponent<PlayerController>().canMove = false;
            m_generator.currentFloor = 0;
            Invoke("ResetPlayer", delayTime);
        }
        //Normal Teleport
        else
        {
            m_transportTarget.transform.position = teleportPosition;
            m_transportTarget.GetComponent<PlayerController>().canMove = false;
            Invoke("ResetPlayer", delayTime);
        }
    }

    public void ResetPlayer()
    {
        m_transportTarget.GetComponent<PlayerController>().canMove = true;
    }

    public void OnDestroy()
    {
        if (m_transportTarget != null)
        {
            ResetPlayer();
        }
    }
}
