using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatReceiver
{
    public CharacterStatReceiver(Character character)
    {
        this.character = character;
    }

    Character character;

    public int atk { get { return character.atk + character.characterBuffHandler.atkBuff; } }
    public float maxCoolTime { get { return character.maxCoolTime - character.characterBuffHandler.speedBuff; } }
}
