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
            character.stateMachine.ChangeState(character.stateMachine.WaitState);
        // ���͵��� ���� ���� Wait���� ����
        for (int i = 0; i < monsters.Count; i++)
        {
            if (!(monsters[i].IsDead))
                monsters[i].stateMachine.ChangeState(monsters[i].stateMachine.WaitState);
        }
        // ���� ����Ʈ�� ���� ���� �Է��� ������ ���¸� Aciton���� ����
        if (performList[0] == 100)
        {
            character.stateMachine.ChangeState(character.stateMachine.ActionState);
        }
        else
        {
            monsters[performList[0]].stateMachine.ChangeState(monsters[performList[0]].stateMachine.ActionState);
        }
        // ���� ����Ʈ���� ����
        performList.RemoveAt(0);
        stateMachine.ChangeState(stateMachine.PerformActionState);
    }
}
