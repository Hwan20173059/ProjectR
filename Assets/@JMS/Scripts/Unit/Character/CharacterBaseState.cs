using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseState : IState
{
    protected CharacterStateMachine stateMachine;
    protected Character character { get { return stateMachine.character; } }
    protected BattleManager battleManager { get { return character.battleManager; } }

    public CharacterBaseState(CharacterStateMachine characterStateMachine)
    {
        stateMachine = characterStateMachine;
    }
    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Debug.DrawRay(character.transform.position, Vector3.right * 4f, new Color(0, 100, 0));
    }

    protected void StateUpdate(string state)
    {
        character.currentStateText = state;
        if (battleManager != null)
            battleManager.battleCanvas.UpdateCharacterState(battleManager.IsRouletteUsed);
    }
}
