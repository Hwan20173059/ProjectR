using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonTutorialManager : MonoBehaviour
{
    public GameObject dungeonTutorial;
    public Image tutorialImage;
    public int imageIndex;
    public int tutorialIndex;

    public Sprite[] dungeonTutorialImage;

    public void ActiveTutorial()
    {
        tutorialImage.sprite = dungeonTutorialImage[0];
        imageIndex = 0;
        tutorialIndex = 0;
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
        }

    }

    public void CloseButton()
    {
        AudioManager.Instance.PlayUISelectSFX();

        dungeonTutorial.SetActive(false);
    }
}