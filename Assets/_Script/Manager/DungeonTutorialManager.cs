using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonTutorialManager : MonoBehaviour
{
    public GameObject dungeonTutorial;
    public GameObject nextButton;
    public GameObject preButton;
    public Image tutorialImage;
    public int imageIndex;
    public int tutorialIndex;

    public Sprite[] dungeonTutorialImage;

    public void ActiveTutorial()
    {
        tutorialImage.sprite = dungeonTutorialImage[0];
        imageIndex = 0;
        tutorialIndex = 0;
        RefreshButton(dungeonTutorialImage.Length);
    }

    public void NextButton()
    {
        AudioManager.Instance.PlayUISelectSFX();

        if (imageIndex + 1 > dungeonTutorialImage.Length - 1)
            return;
        else
        {
            imageIndex++;
            tutorialImage.sprite = dungeonTutorialImage[imageIndex];
            RefreshButton(dungeonTutorialImage.Length);
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
            tutorialImage.sprite = dungeonTutorialImage[imageIndex];
            RefreshButton(dungeonTutorialImage.Length);
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

        dungeonTutorial.SetActive(false);
    }
}