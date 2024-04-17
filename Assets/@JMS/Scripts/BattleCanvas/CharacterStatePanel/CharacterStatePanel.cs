using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterStatePanel : MonoBehaviour
{
    CharacterActionBar characterActionBar;
    CharacterStateText characterStateText;

    public void Init()
    {
        characterActionBar = GetComponentInChildren<CharacterActionBar>();
        characterStateText = GetComponentInChildren<CharacterStateText>();
    }

    public void UpdateActionBar(Character character)
    {
        if (character != null)
            characterActionBar.actionBar.transform.localScale =
                new Vector3(Mathf.Clamp(character.curCoolTime / character.maxCoolTime, 0, character.maxCoolTime), 1, 1);
    }

    public void UpdateCharacterState(Character character)
    {
        if (character == null) return;
        characterStateText.text = $"ĳ���� : {character.characterName}\n���� : {character.level}\n" +
            $"ü�� : {character.curHP} / {character.maxHP}\n���ݷ� : {character.addBuffAtk}\n" +
            $"����ġ : {character.curExp} / {character.needExp}\n���� : {character.currentStateText}";
    }

    public void UpdateCharacterState(Character character, BattleManager battlemanager)
    {
        if (character == null) return;
        characterStateText.text = $"ĳ���� : {character.characterName}\n���� : {character.level}\n" +
            $"ü�� : {character.curHP} / {character.maxHP}\n���ݷ� : <#FF0000>{battlemanager.GetChangeValue(character.addBuffAtk)}</color>\n" +
            $"����ġ : {character.curExp} / {character.needExp}\n���� : {character.currentStateText}";
    }
}
