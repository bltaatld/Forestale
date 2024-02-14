using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject TargetUI;
    public GameObject UI_interact;
    public string[] lines;
    public float textSpeed;
    public float clickSpeed;
    public bool isCilcked;
    public bool triggerActive;
    public bool isTalk;
    private int index;

    void Start()
    {
        TargetUI.SetActive(false);
        startDialogue();
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                TargetUI.SetActive(true);
                UI_interact.SetActive(false);

                if (textComponent.text == lines[index] || index == 0)
                {
                    NextLine();
                }
                else
                {
                    isTalk = false;
                    StopAllCoroutines();
                    StartCoroutine(WaitClickTime(clickSpeed));
                    textComponent.text = lines[index];
                }
            }
        }
        else
        {
            index = 0;
            TargetUI.SetActive(false);
            isCilcked = false;
        }
    }

    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator WaitClickTime(float i)
    {
        yield return new WaitForSeconds(i);
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            isTalk = true;
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            TargetUI.SetActive(false);
            index = 0;
            isTalk = false;
            Debug.Log("Text Ended");
        }
    }

    public void InstantEnd()
    {
        TargetUI.SetActive(false);
        index = 0;
        isTalk = false;
        Debug.Log("Text Ended");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UI_interact.SetActive(true);
            triggerActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UI_interact.SetActive(false);
            triggerActive = false;
        }
    }
}
