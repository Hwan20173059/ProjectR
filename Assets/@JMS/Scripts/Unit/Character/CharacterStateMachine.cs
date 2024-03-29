using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    public Character character { get; }
    public CharacterWaitState waitState { get; }
    public CharacterReadyState readyState { get; }
    public CharacterSelectActionState selectActionState { get; }
    public CharacterActionState actionState { get; }
    public CharacterDeadState deadState { get; }
    public CharacterAutoSelectState autoSelectState { get; }
    public CharacterStateMachine(Character character)
    {
        this.character = character;

        waitState = new CharacterWaitState(this);
        readyState = new CharacterReadyState(this);
        selectActionState = new CharacterSelectActionState(this);
        actionState = new CharacterActionState(this);
        deadState = new CharacterDeadState(this);
        autoSelectState = new CharacterAutoSelectState(this);
    }
}
