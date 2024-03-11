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
        stateMachine.BattleManager.characterPrevState = stateMachine.BattleManager.Character.stateMachine.currentState;
        // ���͵��� ���� ���� ����
        for (int i = 0; i < stateMachine.BattleManager.Monsters.Count; i++)
        {
            stateMachine.BattleManager.monstersPrevState[i] = stateMachine.BattleManager.Monsters[i].stateMachine.currentState;
        }
        // ĳ���� ���� ���� Wait���� ����
        stateMachine.BattleManager.Character.stateMachine.ChangeState(stateMachine.BattleManager.Character.stateMachine.WaitState);
        // ���͵��� ���� ���� Wait���� ����
        for (int i = 0; i < stateMachine.BattleManager.Monsters.Count; i++)
        {
            stateMachine.BattleManager.Monsters[i].stateMachine.ChangeState(stateMachine.BattleManager.Monsters[i].stateMachine.WaitState);
        }
        // ���� ����Ʈ�� ���� ���� �Է��� ������ ���¸� Aciton���� ����
        if (stateMachine.BattleManager.PerformList[0] == 100)
        {
            stateMachine.BattleManager.Character.stateMachine.ChangeState(stateMachine.BattleManager.Character.stateMachine.ActionState);
        }
        else
        {
            stateMachine.BattleManager.Monsters[stateMachine.BattleManager.PerformList[0]].stateMachine.
                ChangeState(stateMachine.BattleManager.Monsters[stateMachine.BattleManager.PerformList[0]].stateMachine.ActionState);
        }
        // ���� ����Ʈ���� ����
        stateMachine.BattleManager.PerformList.RemoveAt(0);
        stateMachine.ChangeState(stateMachine.PerformActionState);
    }
}
