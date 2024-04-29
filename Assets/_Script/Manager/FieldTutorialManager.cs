using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldTutorialManager : MonoBehaviour
{
    public GameObject fieldTutorial;
    public GameObject nextButton;
    public GameObject preButton;
    public Image tutorialImage;
    public int imageIndex;
    public int tutorialIndex;

    public Sprite[] fieldTutorialImage;

    public void ActiveTutorial()
    {
        tutorialImage.sprite = fieldTutorialImage[0];
        imageIndex = 0;
        tutorialIndex = 0;
        RefreshButton(fieldTutorialImage.Length);
    }

    public void NextButton()
    {
        AudioManager.Instance.PlayUISelectSFX();

        if (imageIndex + 1 > fieldTutorialImage.Length - 1)
            return;
        else
        {
            imageIndex++;
            tutorialImage.sprite = fieldTutorialImage[imageIndex];
            RefreshButton(fieldTutorialImage.Length);
        }
    }

    public void PreButton()
    {
        AudioManager.Instance.PlayUISelectSFX();

        if (imageIndex - 1 < 0)
            return;
        else
        {
            imageIndex--;
            tutorialImage.sprite = fieldTutorialImage[imageIndex];
            RefreshButton(fieldTutorialImage.Length);
        }

    }

    public void RefreshButton(int index)
    {
        if (imageIndex == 0)
        {
            preButton.SetActive(false);
            nextButton.SetActive(true);
        }
        else if (imageIndex > 0 && imageIndex < index - 1)
        {
            preButton.SetActive(true);
            nextButton.SetActive(true);
        }
        else if (imageIndex == index - 1)
        {
            preButton.SetActive(true);
            nextButton.SetActive(false);
        }
    }

    public void CloseButton()
    {
        AudioManager.Instance.PlayUISelectSFX();

        fieldTutorial.SetActive(false);
    }
}
