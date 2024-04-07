using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject TargetUI;
    public GameObject TargetUI2;
    public string[] lines;
    public float textSpeed;
    public float clickSpeed;
    public bool Check;

    public float currentDelayTime = 0.0f;
    public float elapsedTime = 0.0f;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        startDialogue();
        textComponent.text = string.Empty;
        Check = false;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= currentDelayTime)
        {
            if (textComponent.text == lines[index])
            {
                elapsedTime = 0.0f;
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(WaitClickTime(clickSpeed));
                textComponent.text = lines[index];
            }
        }
    }

    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    public void SetCheckFalse(bool setbool)
    {
        Check = setbool;
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
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            TargetUI.SetActive(false);
            TargetUI2.SetActive(true);
            index = 0;
            Check = true;
            Debug.Log("Text Ended");
        }
    }
}
