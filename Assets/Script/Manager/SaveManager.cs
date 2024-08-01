using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public Vector3 currentSavePosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void currentPositionSave(Transform currentPosition)
    {
        currentSavePosition = currentPosition.position;
    }

    public void currentPositionLoad(Transform currentPosition)
    {
        currentPosition.position = currentSavePosition;
    }
}
