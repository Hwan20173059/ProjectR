using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerSelectingActionState : BattleBaseState
{
    public BattlePlayerSelectingActionState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        battleCanvas.BattleStateUpdate("�÷��̾��� ������ ��ٸ��� ���Դϴ�.");

        // ���͵��� ���� ���� ����
        for (int i = 0; i < monsters.Count; i++)
        {
            monstersPrevState[i] = monsters[i].stateMachine.currentState;
        }
        // ���͵��� ���� ���� Wait���� ����
        for (int i = 0; i < monsters.Count; i++)
        {
            if (!(monsters[i].IsDead))
                monsters[i].stateMachine.ChangeState(monsters[i].stateMachine.waitState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        for (int i = 0; i < monsters.Count; i++)
        {
            if (!(monsters[i].IsDead))
            {
                // ���͵��� ���¸� ���� ���·� ����
                monsters[i].stateMachine.ChangeState(monstersPrevState[i]);
            }
        }
    }
}
