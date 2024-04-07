using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Image dialgoueMark;
    public GameObject TargetUI;
    public GameObject InGameUI;
    public GameObject UI_interact;
    public Dialogue dialogueSelf;
    public Animator animator;
    public string[] lines;
    public float textSpeed;
    public float clickSpeed;
    public bool isCilcked;
    public bool triggerActive;
    public bool isTalk;
    public bool isStartActive;
    public bool isEnded;
    public bool isNeedInstantEnd;
    public bool isNeedEndAnimation;
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
            if (isEnded)
            {
                InGameUI.SetActive(true);
            }
            if (!isEnded)
            {
                InGameUI.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Z) || isStartActive)
            {
                dialgoueMark.gameObject.SetActive(false);
                TargetUI.SetActive(true);
                UI_interact.SetActive(false);

                if (textComponent.text == lines[index] || index == 0)
                {
                    NextLine();
                    isStartActive = false;
                }
                else
                {
                    dialgoueMark.gameObject.SetActive(true);
                    isTalk = false;
                    StopAllCoroutines();
                    StartCoroutine(WaitClickTime(clickSpeed));
                    textComponent.text = lines[index];
                    isStartActive = false;
                }
            }
        }
        else
        {
            index = 0;
            TargetUI.SetActive(false);
            isCilcked = false;
        }

        if (isNeedInstantEnd)
        {
            if (isEnded)
            {
                dialogueSelf.enabled = false;
            }
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
        int lineLength = lines[index].Length;
        for (int i = 0; i < lineLength; i++)
        {
            textComponent.text += lines[index][i];
            if (i == lineLength - 1)
            {
                dialgoueMark.gameObject.SetActive(true);
            }
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
            isEnded = true;

            if (isNeedEndAnimation)
            {
                animator.SetTrigger("DialogueEnd");
            }
            
            Debug.Log("Text Ended");
        }
    }

    public void InstantEnd()
    {
        TargetUI.SetActive(false);
        index = 0;
        isTalk = false;
        isEnded = true;
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
            isTalk = false;
            triggerActive = false;
        }
    }
}
