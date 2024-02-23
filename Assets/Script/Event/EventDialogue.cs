using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventDialogue : MonoBehaviour
{
    public string id;
    public PlayerController controller;
    public TextMeshProUGUI textComponent;
    public GameObject TargetUI;
    public string[] lines;
    public float textSpeed;
    public float clickSpeed;
    public bool triggerActive;
    public bool isTalk;
    public bool isFirst;
    public bool isEnd;
    public int index;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        TargetUI.SetActive(false);
        startDialogue();
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive)
        {
            if (!isFirst)
            {
                TargetUI.SetActive(true);
                NextLine();
                isFirst = true;
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (index == lines.Length - 1)
                {
                    index = 0;
                    TargetUI.SetActive(false);
                    isEnd = true;

                    controller.systemValue.Prograss.Add(id);
                }

                if (textComponent.text == lines[index] || index == 0 && !isEnd)
                {
                    NextLine();
                }

                else if (!isEnd)
                {
                    isTalk = false;
                    StopAllCoroutines();
                    StartCoroutine(WaitClickTime(clickSpeed));
                    textComponent.text = lines[index];
                }
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
            triggerActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }
}
