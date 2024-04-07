using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSwap : MonoBehaviour
{
    public bool isEnded;
    public Dialogue firstDialogue;
    public Dialogue secondDialogue;

    private void Update()
    {
        if(firstDialogue.isEnded && !isEnded)
        {
            firstDialogue.enabled = false;
            secondDialogue.enabled = true;
            isEnded = true;
        }
    }
}
