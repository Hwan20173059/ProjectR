using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldTutorialManager : MonoBehaviour
{
    public GameObject fieldTutorial;
    public Image tutorialImage;
    public int imageIndex;
    public int tutorialIndex;

    public Sprite[] fieldTutorialImage;

    public void ActiveTutorial()
    {
        tutorialImage.sprite = fieldTutorialImage[0];
        imageIndex = 0;
        tutorialIndex = 0;
    }

    public void NextButton()
    {
        if (imageIndex + 1 > fieldTutorialImage.Length - 1)
            return;
        else
        {
            imageIndex++;
            tutorialImage.sprite = fieldTutorialImage[imageIndex];
        }
    }

    public void PreButton()
    {
        if (imageIndex - 1 < 0)
            return;
        else
        {
            imageIndex--;
            tutorialImage.sprite = fieldTutorialImage[imageIndex];
        }

    }

    public void CloseButton()
    {
        fieldTutorial.SetActive(false);
    }
}
