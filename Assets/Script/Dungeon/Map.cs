using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Room focusedRoom
    {
        get => _focusedRoom;
        set
        {
            _focusedRoom = value;
            Debug.Log(_focusedRoom);
        }
    }

    private Room _focusedRoom;

    public bool roomCleared
    {
        get => _roomCleared;
        set
        {
            _roomCleared = value;
            foreach (Door i in focusedRoom.doors)
            {
                if (i.targetDoor != null)
                {
                    i.opened = roomCleared;
                }

                if (roomCleared == true)
                {
                    mapGenerator.ConnectRoomDoors();
                }
            }
            Debug.Log("door open");
        }
    }
    private bool _roomCleared;

    private MapGenerator mapGenerator;

    void Awake() 
    {
        mapGenerator = GetComponent<MapGenerator>();
    }


    void Start()
    {
        mapGenerator.Generate();
        focusedRoom = mapGenerator.rooms[mapGenerator.startingPosition.x, mapGenerator.startingPosition.y];
    }

    private void EnemySpawn()
    {
        Transform[] enemies = focusedRoom.GetComponentsInChildren<Transform>(true);
        foreach (Transform objects in enemies)
        {
            if (objects.name == "Enemy")
            {
                objects.gameObject.SetActive(true);
            }
        }
    }

    public void CheckRoom()
    {
        EnemySpawn();        
        var cleared = true;

        Transform[] enemies = focusedRoom.GetComponentsInChildren<Transform>(true);
        foreach (Transform objects in enemies)
        {
            if (objects.CompareTag("EnemyMain"))
            {
                TE_EnemyChase enemyChase = objects.GetComponent<TE_EnemyChase>();
                Debug.Log(objects.GetComponent<TE_EnemyChase>());
                if (enemyChase != null && enemyChase.enemyHealth > 0)
                {
                    cleared = false;
                    break;
                }
            }
        }

        roomCleared = cleared;
    }
}
