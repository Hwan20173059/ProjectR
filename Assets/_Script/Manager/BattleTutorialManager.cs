using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTutorialManager : MonoBehaviour
{
    public GameObject battleTutorial;
    public GameObject nextButton;
    public GameObject preButton;
    public Image tutorialImage;
    public int imageIndex;
    public int tutorialIndex;

    public Sprite[] battleTutorialImage;

    private void Start()
    {
        if (PlayerManager.Instance.firstBattle == true)
        {
            PlayerManager.Instance.firstBattle = false;
            ActiveTutorial();
        }
        else 
        {
            this.gameObject.SetActive(false); 
        }
    }

    public void ActiveTutorial()
    {
        tutorialImage.sprite = battleTutorialImage[0];
        imageIndex = 0;
        tutorialIndex = 0;
        RefreshButton(battleTutorialImage.Length);
    }

    public void NextButton()
    {
        AudioManager.Instance.PlayUISelectSFX();

        if (imageIndex + 1 > battleTutorialImage.Length - 1)
            return;
        else
        {
            imageIndex++;
            tutorialImage.sprite = battleTutorialImage[imageIndex];
            RefreshButton(battleTutorialImage.Length);
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
            tutorialImage.sprite = battleTutorialImage[imageIndex];
            RefreshButton(battleTutorialImage.Length);
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

        battleTutorial.SetActive(false);
    }
}