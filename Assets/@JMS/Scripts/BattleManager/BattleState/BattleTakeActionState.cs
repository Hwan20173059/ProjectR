using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTakeActionState : BattleBaseState
{
    public BattleTakeActionState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        // ĳ���� ���� ���� ����
        characterPrevState = character.stateMachine.currentState;
        // ���͵��� ���� ���� ����
        for (int i = 0; i < monsters.Count; i++)
        {
            monstersPrevState[i] = monsters[i].stateMachine.currentState;
        }
        // ĳ���� ���� ���� Wait���� ����
        if (!(character.IsDead))
            character.stateMachine.ChangeState(character.stateMachine.waitState);
        // ���͵��� ���� ���� Wait���� ����
        for (int i = 0; i < monsters.Count; i++)
        {
            if (!(monsters[i].IsDead))
                monsters[i].stateMachine.ChangeState(monsters[i].stateMachine.waitState);
        }
        // ���� ����Ʈ�� ���� ���� �Է��� ������ ���¸� Aciton���� ����
        if (performList[0] == 100)
        {
            character.stateMachine.ChangeState(character.stateMachine.actionState);
        }
        else
        {
            monsters[performList[0]].stateMachine.ChangeState(monsters[performList[0]].stateMachine.actionState);
        }

        stateMachine.ChangeState(stateMachine.performActionState);
    }
}
