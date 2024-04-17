using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class RouletteMachine : MonoBehaviour
{
    BattleManager battleManager;

    public Roulette0 roulette0;
    public Roulette1 roulette1;
    public Roulette2 roulette2;

    float rouletteSpeed = 3000f;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;

        roulette0 = GetComponentInChildren<Roulette0>();
        roulette1 = GetComponentInChildren<Roulette1>();
        roulette2 = GetComponentInChildren<Roulette2>();

        roulette0.Init(0);
        roulette1.Init(1);
        roulette2.Init(2);
    }

    public void SetRoulette(int resultIndex0, int resultIndex1, int resultIndex2)
    {
        StartCoroutine(Roulette(resultIndex0, resultIndex1, resultIndex2));
    }

    IEnumerator Roulette(int resultIndex0, int resultIndex1, int resultIndex2)
    {
        StartCoroutine(SpinRoulette(roulette0, 6, resultIndex0));
        StartCoroutine(SpinRoulette(roulette1, 10, resultIndex1));
        yield return StartCoroutine(SpinRoulette(roulette2, 14, resultIndex2));
        
        battleManager.IsUsingRoulette = false;

        battleManager.battleCanvas.UpdateCharacterAtk();

        if (battleManager.IsAutoBattle)
            battleManager.OnClickAttackButton();
    }

    IEnumerator SpinRoulette(RouletteBase roulette, int spinCount, int resultIndex)
    {
        for (int i = 0; i < spinCount; i++)
        {
            while (OneSpinRoulette(roulette)) { yield return null; }
            roulette.transform.localPosition = roulette.startPosition;
        }

        while (ResultRoulette(roulette, resultIndex)) {  yield return null; }
    }

    bool OneSpinRoulette(RouletteBase roulette)
    {
        return roulette.endPosition != (roulette.transform.localPosition =
            Vector3.MoveTowards(roulette.transform.localPosition, roulette.endPosition, rouletteSpeed * Time.deltaTime));
    }

    bool ResultRoulette(RouletteBase roulette, int resultIndex)
    {
        if(resultIndex == 1)
        {
            return roulette.resultPosition1 != (roulette.transform.localPosition =
                Vector3.MoveTowards(roulette.transform.localPosition, roulette.resultPosition1, rouletteSpeed * Time.deltaTime));
        }
        else if(resultIndex == 2)
        {
            return roulette.resultPosition2 != (roulette.transform.localPosition =
                Vector3.MoveTowards(roulette.transform.localPosition, roulette.resultPosition2, rouletteSpeed * Time.deltaTime));
        }
        else
        {
            return false;
        }
    }
}
