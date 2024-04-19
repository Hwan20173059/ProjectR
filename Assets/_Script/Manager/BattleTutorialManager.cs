using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTutorialManager : MonoBehaviour
{
    public GameObject battleTutorial;
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
        }

    }

    public void CloseButton()
    {
        AudioManager.Instance.PlayUISelectSFX();

        battleTutorial.SetActive(false);
    }
}