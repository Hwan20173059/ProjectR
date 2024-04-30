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
        characterStateText.text = $"캐릭터 : {character.characterName}\n레벨 : {character.level}\n" +
            $"체력 : {character.curHP} / {character.maxHP}\n공격력 : {character.addBuffAtk}\n" +
            $"경험치 : {character.curExp} / {character.needExp}\n상태 : {character.currentStateText}";
    }

    public void UpdateCharacterState(Character character, BattleManager battlemanager)
    {
        if (character == null) return;
        characterStateText.text = $"캐릭터 : {character.characterName}\n레벨 : {character.level}\n" +
            $"체력 : {character.curHP} / {character.maxHP}\n공격력 : <#FF0000>{battlemanager.GetRouletteValue(character.addBuffAtk)}</color>\n" +
            $"경험치 : {character.curExp} / {character.needExp}\n상태 : {character.currentStateText}";
    }
}
