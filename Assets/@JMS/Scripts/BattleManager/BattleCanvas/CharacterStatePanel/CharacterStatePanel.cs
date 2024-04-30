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

        characterActionBar.Init();
        characterStateText.Init();
    }

    public void UpdateActionBar(Character character)
    {
        characterActionBar.UpdateActionBar(character);
    }

    public void UpdateCharacterState(Character character)
    {
        characterStateText.UpdateCharacterState(character);
    }

    public void UpdateCharacterState(Character character, BattleManager battlemanager)
    {
        characterStateText.UpdateCharacterState(character, battlemanager);
    }
}
