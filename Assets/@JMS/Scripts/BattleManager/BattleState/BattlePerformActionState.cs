using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePerformActionState : BattleBaseState
{
    public BattlePerformActionState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }
    public override void Exit()
    {
        base.Exit();
        if (!(character.IsDead))
        {
             // ĳ���� ���¸� ���� ���·� ����
            character.stateMachine.ChangeState(characterPrevState);
        }

        for (int i = 0; i < monsters.Count; i++)
        {
            if (!(monsters[i].IsDead))
            {
                // ���͵��� ���¸� ���� ���·� ����
                monsterPrevStateIndex = i;
                monsters[i].stateMachine.ChangeState(monsterPrevState);
            }
        }
    }
}
