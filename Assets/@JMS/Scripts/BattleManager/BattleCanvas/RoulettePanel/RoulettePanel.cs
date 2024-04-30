using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulettePanel : MonoBehaviour
{
    RouletteMachine rouletteMachine;

    public void Init(BattleManager battleManager)
    {
        rouletteMachine = GetComponentInChildren<RouletteMachine>();

        rouletteMachine.Init(battleManager);
    }

    public void SetRoulette(int resultIndex0, int resultIndex1, int resultIndex2)
    {
        rouletteMachine.SetRoulette(resultIndex0, resultIndex1, resultIndex2);
    }
}
