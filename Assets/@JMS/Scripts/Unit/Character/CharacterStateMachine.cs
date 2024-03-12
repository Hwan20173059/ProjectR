using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    public Character Character { get; }
    public CharacterWaitState WaitState { get; }
    public CharacterReadyState ReadyState { get; }
    public CharacterSelectActionState SelectActionState { get; }
    public CharacterActionState ActionState { get; }
    public CharacterDeadState DeadState { get; }
    public CharacterStateMachine(Character character)
    {
        Character = character;

        WaitState = new CharacterWaitState(this);
        ReadyState = new CharacterReadyState(this);
        SelectActionState = new CharacterSelectActionState(this);
        ActionState = new CharacterActionState(this);
        DeadState = new CharacterDeadState(this);
    }
}
