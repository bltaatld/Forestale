using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadToZ : MonoBehaviour
{
    public GameObject targetButtonParent;
    public GameObject targetButton;
    public EventManager eventManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            targetButtonParent.SetActive(true);
            eventManager.SetSelected(targetButton);
            gameObject.SetActive(false);
        }
    }
}
