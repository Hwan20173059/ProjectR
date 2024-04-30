using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStateText : MonoBehaviour
{
    TextMeshProUGUI characterStateText;

    public void Init()
    {
        characterStateText = GetComponent<TextMeshProUGUI>();
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
            $"ü�� : {character.curHP} / {character.maxHP}\n���ݷ� : <#FF0000>{battlemanager.GetRouletteValue(character.addBuffAtk)}</color>\n" +
            $"����ġ : {character.curExp} / {character.needExp}\n���� : {character.currentStateText}";
    }
}
