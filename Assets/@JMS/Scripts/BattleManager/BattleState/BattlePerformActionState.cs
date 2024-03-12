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
        // ĳ���� ���¸� ���� ���·� ����
        if (!(stateMachine.BattleManager.Character.stateMachine.currentState is CharacterDeadState))
            stateMachine.BattleManager.Character.stateMachine.ChangeState(stateMachine.BattleManager.characterPrevState);
        // ���͵��� ���¸� ���� ���·� ����
        for (int i = 0; i < stateMachine.BattleManager.Monsters.Count; i++)
        {
            if (!(stateMachine.BattleManager.Monsters[i].stateMachine.currentState is MonsterDeadState))
                stateMachine.BattleManager.Monsters[i].stateMachine.ChangeState(stateMachine.BattleManager.monstersPrevState[i]);
        }
    }
}
