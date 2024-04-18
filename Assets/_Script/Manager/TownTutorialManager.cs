using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownTutorialManager : MonoBehaviour
{
    public GameObject townTutorial;
    public Image tutorialImage;
    public int imageIndex;
    public int tutorialIndex;

    public Sprite[] gameTutorial;
    public Sprite[] characterTutorial;
    public Sprite[] equipTutorial;
    public Sprite[] inventoryTutorial;
    public Sprite[] guildTutorial;
    public Sprite[] shopTutorial;
    public Sprite[] gochaTutorial;

    public void ActiveTutorial(int index)
    {
        switch (index)
        {
            case 0:
                tutorialImage.sprite = gameTutorial[0];
                imageIndex = 0;
                tutorialIndex = 0;
                break;
            case 1:
                tutorialImage.sprite = characterTutorial[0];
                imageIndex = 0;
                tutorialIndex = 1;
                break;
            case 2:
                tutorialImage.sprite = equipTutorial[0];
                imageIndex = 0;
                tutorialIndex = 2;
                break;
            case 3:
                tutorialImage.sprite = inventoryTutorial[0];
                imageIndex = 0;
                tutorialIndex = 3;
                break;
            case 4:
                tutorialImage.sprite = guildTutorial[0];
                imageIndex = 0;
                tutorialIndex = 4;
                break;
            case 5:
                tutorialImage.sprite = shopTutorial[0];
                imageIndex = 0;
                tutorialIndex = 5;
                break;
            case 6:
                tutorialImage.sprite = gochaTutorial[0];
                imageIndex = 0;
                tutorialIndex = 6;
                break;
        }
    }

    public void NextButton()
    {
        AudioManager.Instance.PlayUISelectSFX();

        switch (tutorialIndex) 
        {
            case 0:
                if (imageIndex + 1 > gameTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    tutorialImage.sprite = gameTutorial[imageIndex];
                }
                break;

            case 1:
                if (imageIndex + 1 > characterTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    tutorialImage.sprite = characterTutorial[imageIndex];
                }
                break;

            case 2:
                if (imageIndex + 1 > equipTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    tutorialImage.sprite = equipTutorial[imageIndex];
                }
                break;

            case 3:
                if (imageIndex + 1 > inventoryTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    tutorialImage.sprite = inventoryTutorial[imageIndex];
                }
                break;

            case 4:
                if (imageIndex + 1 > guildTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    tutorialImage.sprite = guildTutorial[imageIndex];
                }
                break;

            case 5:
                if (imageIndex + 1 > shopTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    tutorialImage.sprite = shopTutorial[imageIndex];
                }
                break;

            case 6:
                if (imageIndex + 1 > gochaTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    tutorialImage.sprite = gochaTutorial[imageIndex];
                }
                break;
        }
        
    }

    public void PreButton()
    {
        AudioManager.Instance.PlayUISelectSFX();

        switch (tutorialIndex)
        {
            case 0:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    tutorialImage.sprite = gameTutorial[imageIndex];
                }
                break;

            case 1:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    tutorialImage.sprite = characterTutorial[imageIndex];
                }
                break;

            case 2:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    tutorialImage.sprite = equipTutorial[imageIndex];
                }
                break;

            case 3:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    tutorialImage.sprite = inventoryTutorial[imageIndex];
                }
                break;

            case 4:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    tutorialImage.sprite = guildTutorial[imageIndex];
                }
                break;

            case 5:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    tutorialImage.sprite = shopTutorial[imageIndex];
                }
                break;

            case 6:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    tutorialImage.sprite = gochaTutorial[imageIndex];
                }
                break;
        }

        
    }

    public void CloseButton()
    {
        AudioManager.Instance.PlayUISelectSFX();

        townTutorial.SetActive(false);
    }
}
