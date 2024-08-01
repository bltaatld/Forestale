using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCutScene : MonoBehaviour
{
    public GameObject SkipHelp;
    public GameObject DialogueUI;
    public GameObject TitleUI;

    private void Start()
    {
        SkipHelp.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            SkipHelp.SetActive(true);
        }

        if (SkipHelp.activeSelf == true && Input.GetKeyDown(KeyCode.Z))
        {
            SkipHelp.SetActive(false);
            DialogueUI.SetActive(false);
            TitleUI.SetActive(true);
        }
    }

    public void StartGame()
    {
        Loader.LoadScene("IntroScene");
    }

    public void LoadGame()
    {
        Loader.LoadScene("ExpoDemo");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
