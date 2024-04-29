using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownTutorialManager : MonoBehaviour
{
    public GameObject townTutorial;
    public GameObject nextButton;
    public GameObject preButton;
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
                RefreshButton(gameTutorial.Length);
                break;
            case 1:
                tutorialImage.sprite = characterTutorial[0];
                imageIndex = 0;
                tutorialIndex = 1;
                RefreshButton(characterTutorial.Length);
                break;
            case 2:
                tutorialImage.sprite = equipTutorial[0];
                imageIndex = 0;
                tutorialIndex = 2;
                RefreshButton(equipTutorial.Length);
                break;
            case 3:
                tutorialImage.sprite = inventoryTutorial[0];
                imageIndex = 0;
                tutorialIndex = 3;
                RefreshButton(inventoryTutorial.Length);
                break;
            case 4:
                tutorialImage.sprite = guildTutorial[0];
                imageIndex = 0;
                tutorialIndex = 4;
                RefreshButton(guildTutorial.Length);
                break;
            case 5:
                tutorialImage.sprite = shopTutorial[0];
                imageIndex = 0;
                tutorialIndex = 5;
                RefreshButton(shopTutorial.Length);
                break;
            case 6:
                tutorialImage.sprite = gochaTutorial[0];
                imageIndex = 0;
                tutorialIndex = 6;
                RefreshButton(gochaTutorial.Length);
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
                    RefreshButton(gameTutorial.Length);
                    tutorialImage.sprite = gameTutorial[imageIndex];
                }
                break;

            case 1:
                if (imageIndex + 1 > characterTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    RefreshButton(characterTutorial.Length);
                    tutorialImage.sprite = characterTutorial[imageIndex];
                }
                break;

            case 2:
                if (imageIndex + 1 > equipTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    RefreshButton(equipTutorial.Length);
                    tutorialImage.sprite = equipTutorial[imageIndex];
                }
                break;

            case 3:
                if (imageIndex + 1 > inventoryTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    RefreshButton(inventoryTutorial.Length);
                    tutorialImage.sprite = inventoryTutorial[imageIndex];
                }
                break;

            case 4:
                if (imageIndex + 1 > guildTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    RefreshButton(guildTutorial.Length);
                    tutorialImage.sprite = guildTutorial[imageIndex];
                }
                break;

            case 5:
                if (imageIndex + 1 > shopTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    RefreshButton(shopTutorial.Length);
                    tutorialImage.sprite = shopTutorial[imageIndex];
                }
                break;

            case 6:
                if (imageIndex + 1 > gochaTutorial.Length - 1)
                    return;
                else
                {
                    imageIndex++;
                    RefreshButton(gochaTutorial.Length);
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
                    RefreshButton(gameTutorial.Length);
                    tutorialImage.sprite = gameTutorial[imageIndex];
                }
                break;

            case 1:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    RefreshButton(characterTutorial.Length);
                    tutorialImage.sprite = characterTutorial[imageIndex];
                }
                break;

            case 2:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    RefreshButton(equipTutorial.Length);
                    tutorialImage.sprite = equipTutorial[imageIndex];
                }
                break;

            case 3:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    RefreshButton(inventoryTutorial.Length);
                    tutorialImage.sprite = inventoryTutorial[imageIndex];
                }
                break;

            case 4:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    RefreshButton(guildTutorial.Length);
                    tutorialImage.sprite = guildTutorial[imageIndex];
                }
                break;

            case 5:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    RefreshButton(shopTutorial.Length);
                    tutorialImage.sprite = shopTutorial[imageIndex];
                }
                break;

            case 6:
                if (imageIndex - 1 < 0)
                    return;
                else
                {
                    imageIndex--;
                    RefreshButton(gochaTutorial.Length);
                    tutorialImage.sprite = gochaTutorial[imageIndex];
                }
                break;
        }

        
    }

    public void RefreshButton(int index)
    {
        if(imageIndex == 0)
        {
            preButton.SetActive(false);
            nextButton.SetActive(true);
        }
        else if(imageIndex > 0 && imageIndex < index - 1)
        {
            preButton.SetActive(true);
            nextButton.SetActive(true);
        }
        else if(imageIndex == index - 1)
        {
            preButton.SetActive(true);
            nextButton.SetActive(false);
        }
    }

    public void CloseButton()
    {
        AudioManager.Instance.PlayUISelectSFX();

        townTutorial.SetActive(false);
    }
}
