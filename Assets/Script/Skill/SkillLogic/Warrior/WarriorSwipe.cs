using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSwipe : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        Vector3 rotation = Vector3.zero;

        if (animator.GetInteger("MoveX") == 1)
        {
            rotation = new Vector3(0, 0, 90);
        }
        if (animator.GetInteger("MoveX") == -1)
        {
            rotation = new Vector3(0, 0, -90);
        }
        if (animator.GetInteger("MoveY") == -1)
        {
            rotation = new Vector3(0, 0, 0);
        }
        if (animator.GetInteger("MoveY") == 1)
        {
            rotation = new Vector3(0, 0, 180);
        }

        transform.eulerAngles = rotation;
    }

    public void DestroySkill()
    {
        Destroy(gameObject);
    }
}
