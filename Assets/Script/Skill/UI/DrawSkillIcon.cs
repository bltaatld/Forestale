using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawSkillIcon : MonoBehaviour
{
    public Image[] currentImage;
    public Image[] targetImage;
    private void Update()
    {
        for(int i =0; i < currentImage.Length;i++)
        {
            if (currentImage[i].sprite != null)
            {
                targetImage[i].gameObject.SetActive(true);
                targetImage[i].sprite = currentImage[i].sprite;
            }
            else
            {
                targetImage[i].gameObject.SetActive(false);
            }
        }
    }
}
