using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSwipe : MonoBehaviour
{
    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null && playerObject.TryGetComponent(out RopeObject playerController))
        {
            Vector3 rotation = Vector3.zero;

            if (playerController.currnetDirection.x > 0)
            {
                rotation = new Vector3(0, 0, 90);
            }
            if (playerController.currnetDirection.x < 0)
            {
                rotation = new Vector3(0, 0, -90);
            }
            if (playerController.currnetDirection.y < 0)
            {
                rotation = new Vector3(0, 0, 0);
            }
            if (playerController.currnetDirection.y > 0)
            {
                rotation = new Vector3(0, 0, 180);
            }

            transform.eulerAngles = rotation;
        }
    }

    public void DestroySkill()
    {
        Destroy(gameObject);
    }
}
