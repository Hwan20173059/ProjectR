using Assets.PixelFantasy.PixelMonsters.Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStunState : MonsterBaseState
{
    public MonsterStunState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }

    public override void Update()
    {
        base.Update();

        monster.StunTimeUpdate();
        monster.BurnUpdate();
    }

}
