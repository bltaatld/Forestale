using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject destroyEffect;
    public Transform holdSpot;
    public LayerMask pickUpMask;
    public Vector3 Direction;
    public float throwSpeed;
    public float throwPower;
    public bool isPush;
    public bool isShop;

    private float epsilon = 0.0001f;
    public GameObject itemHolding;

    void Update()
    {

        if (playerController.moveDirection != Vector3.zero && (Mathf.Abs(playerController.moveDirection.x - Mathf.Round(playerController.moveDirection.x)) < epsilon))
        {
            Direction = playerController.moveDirection;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (itemHolding && !isShop)
            {
                playerController.animator.SetBool("IsHold", false);
            }
            else
            {
                Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, 0.3f, pickUpMask);
                if (pickUpItem)
                {
                    playerController.animator.SetBool("IsHold", true);
                }
            }

        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            if (itemHolding)
            {
                playerController.animator.SetBool("IsHold", false);
                StartCoroutine(ThrowItem(itemHolding));
                itemHolding = null;
            }
        }
    }

    public void UnHoldItem()
    {
        itemHolding.transform.position = transform.position + Direction;
        itemHolding.transform.parent = null;
        if (itemHolding.GetComponent<Rigidbody2D>())
            itemHolding.GetComponent<Rigidbody2D>().simulated = true;
        itemHolding = null;
    }

    public void HoldItem()
    {
        Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, 0.3f, pickUpMask);
        if (pickUpItem)
        {
            Debug.Log(pickUpItem.gameObject);
            itemHolding = pickUpItem.gameObject;
            itemHolding.transform.position = holdSpot.position;
            itemHolding.transform.parent = holdSpot.transform;
            Debug.Log(itemHolding);
            if (itemHolding.GetComponent<Rigidbody2D>())
                itemHolding.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    IEnumerator ThrowItem(GameObject item)
    {
        Vector3 startPoint = item.transform.position;
        Vector3 endPoint = transform.position + Direction * throwPower;
        item.transform.parent = null;
        for (int i = 0; i < 25; i++)
        {
            item.transform.position = Vector3.Lerp(startPoint, endPoint, i * throwSpeed);
            yield return null;
        }
        if (item.GetComponent<Rigidbody2D>())
            item.GetComponent<Rigidbody2D>().simulated = true;
        Instantiate(destroyEffect, item.transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Rock"))
        {
            isPush = true;
        }
        if (collision.collider.CompareTag("Shop"))
        {
            isShop = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Rock"))
        {
            isPush = false;
        }
        if (collision.collider.CompareTag("Shop"))
        {
            isShop = false;
        }
    }
}