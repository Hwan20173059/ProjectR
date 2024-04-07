using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    PlayerManager playerManager;

    public Image characterImage;
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
        Character character = playerManager.characterList[playerManager.selectedCharacterIndex];

        characterImage.sprite = character.sprite;
        characterImage.SetNativeSize();

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
