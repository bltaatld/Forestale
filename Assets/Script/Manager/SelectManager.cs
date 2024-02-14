using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public bool selection;

    public void isYes()
    {
        selection = true;
    }

    public void isNo()
    {
        selection = false;
    }
}
