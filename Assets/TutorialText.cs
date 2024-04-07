using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public GameObject tutorialObject;
    public GameObject currentTutorial;
    private bool checkActive;
    private bool isTriggered;

    private void Update()
    {
        if (!checkActive && isTriggered)
        {
            Time.timeScale = 0f;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                tutorialObject.SetActive(false);
                currentTutorial.SetActive(false);
                Time.timeScale = 1f;
                checkActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !checkActive)
        {
            isTriggered = true;
            tutorialObject.SetActive(true);
            currentTutorial.SetActive(true);
        }
    }
}
