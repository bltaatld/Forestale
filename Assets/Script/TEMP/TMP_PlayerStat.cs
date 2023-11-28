using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TMP_PlayerStat : MonoBehaviour
{
    public int playerHP;
    public int playerAP;
    public Image HP_image;
    public Sprite[] HP_images;

    public SpriteRenderer currentSprite;
    public SpriteRenderer shipSprite;
    public GameObject ship;

    private void Update()
    {
        HPUpate(playerHP);

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0)
        {
            shipSprite.flipX = true;
        }

        if (horizontalInput < 0)
        {
            shipSprite.flipX = false;
        }

        if (playerHP <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void HPUpate(int playerHP)
    {
        HP_image.sprite = HP_images[playerHP];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            playerHP -= 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Sea"))
        {
            currentSprite.enabled = false;
            ship.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Sea"))
        {
            currentSprite.enabled = true;
            ship.SetActive(false);
        }
    }
}
