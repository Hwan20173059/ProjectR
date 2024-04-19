using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    PlayerManager playerManager;

    public Image characterImage;
    public TextMeshProUGUI characterGrade;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterLevel;
    public TextMeshProUGUI characterHP;
    public TextMeshProUGUI characterATK;
    public TextMeshProUGUI characterSPD;
    public Slider expSlider;
    public TextMeshProUGUI expText;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        CharacterInfoUIRefresh();
    }

    public void CharacterUIon()
    {
        this.gameObject.SetActive(true);
    }

    public void CharacterUIoff()
    {
        this.gameObject.SetActive(false);
    }

    public void CharacterInfoUIRefresh()
    {
        if (playerManager == null) playerManager = PlayerManager.Instance;
        Character character = playerManager.characterList[playerManager.selectedCharacterIndex];

        characterImage.sprite = character.sprite;
        characterImage.SetNativeSize();

        if (character.baseData.grade == 0)
            characterGrade.text = "<color=black>ÀÏ¹Ý</color>";
        else if (character.baseData.grade == 1)
            characterGrade.text = "<color=blue>Èñ±Í</color>";
        else if (character.baseData.grade == 2)
            characterGrade.text = "<color=purple>¿µ¿õ</color>";

        characterName.text = character.characterName;
        characterLevel.text = "Lv. " + character.level;

        characterHP.text = character.curHP + " / " + character.maxHP;
        characterATK.text = character.atk.ToString();

        string speed = "";
        for (int i = 0; i < 6 - character.maxCoolTime ; i++)
            speed += "¡Ù";
        characterSPD.text = speed;

        expSlider.value = (float)character.curExp / (float)character.needExp;
        expText.text = character.curExp + " / " + character.needExp;
    }
}
