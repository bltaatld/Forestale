using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour
{
    public void SetSelected(GameObject targetObject)
    {
        EventSystem.current.SetSelectedGameObject(targetObject);
    }
}