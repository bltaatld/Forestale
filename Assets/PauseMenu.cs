using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuTarget;
    public GameObject targetStartSelect;
    public EventManager eventManager;
    public PlayerController playerController;
    private bool isActive;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isActive)
            {
                OpenMenu();
            }
            else
            {
                RestartMenu();
            }
        }
    }

    public void OpenMenu()
    {
        isActive = true;

        playerController.canMove = false;
        menuTarget.SetActive(true);
        eventManager.SetSelected(targetStartSelect);

        Time.timeScale = 0f;
    }

    public void RestartMenu()
    {
        isActive = false;

        playerController.canMove = true;
        menuTarget.SetActive(false);

        Time.timeScale = 1f;
    }
}
