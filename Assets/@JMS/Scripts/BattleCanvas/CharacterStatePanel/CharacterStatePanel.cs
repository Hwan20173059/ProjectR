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
        characterStateText.text = $"캐릭터 : {character.characterName}\n레벨 : {character.level}\n" +
            $"체력 : {character.curHP} / {character.maxHP}\n공격력 : {character.addBuffAtk}\n" +
            $"경험치 : {character.curExp} / {character.needExp}\n상태 : {character.currentStateText}";
    }

    public void UpdateCharacterState(Character character, BattleManager battlemanager)
    {
        if (character == null) return;
        characterStateText.text = $"캐릭터 : {character.characterName}\n레벨 : {character.level}\n" +
            $"체력 : {character.curHP} / {character.maxHP}\n공격력 : <#FF0000>{battlemanager.GetChangeValue(character.addBuffAtk)}</color>\n" +
            $"경험치 : {character.curExp} / {character.needExp}\n상태 : {character.currentStateText}";
    }
}
