using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    public CharacterJMS Character { get; }
    public CharacterWaitState WaitState { get; }
    public CharacterReadyState ReadyState { get; }
    public CharacterSelectActionState SelectActionState { get; }
    public CharacterSelectTargetState SelectTargetState { get; }
    public CharacterActionState ActionState { get; }
    public CharacterDeadState DeadState { get; }
    public CharacterStateMachine(CharacterJMS character)
    {
        Character = character;

        WaitState = new CharacterWaitState(this);
        ReadyState = new CharacterReadyState(this);
        SelectActionState = new CharacterSelectActionState(this);
        SelectTargetState = new CharacterSelectTargetState(this);
        ActionState = new CharacterActionState(this);
        DeadState = new CharacterDeadState(this);
    }
}
