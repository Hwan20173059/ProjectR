using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine(ReduceSpeedRoulette(resultIndex0, resultIndex1, resultIndex2));
        // FixedSpeedRoulette or ReduceSpeedRoulette
        // To operate the FixedSpeedRoulette, MoveTowards was used.
        // To operate the ReduceSpeedRoulette, Lerp was used.
    }

    IEnumerator FixedSpeedRoulette(int resultIndex0, int resultIndex1, int resultIndex2)
    {
        StartCoroutine(MoveTowardsSpinRoulette(roulette0, 6, resultIndex0));
        StartCoroutine(MoveTowardsSpinRoulette(roulette1, 10, resultIndex1));
        yield return StartCoroutine(MoveTowardsSpinRoulette(roulette2, 14, resultIndex2));

        battleManager.IsUsingRoulette = false;

        battleManager.battleCanvas.UpdateCharacterAtk();

        if (battleManager.IsAutoBattle)
            battleManager.OnClickAttackButton();
    }

    IEnumerator MoveTowardsSpinRoulette(RouletteBase roulette, int spinCount, int resultIndex)
    {
        for (int i = 0; i < spinCount; i++)
        {
            while (OneSpinRoulette(roulette)) { yield return null; }
            roulette.transform.localPosition = roulette.startPosition;
        }

        while (ResultRoulette(roulette, resultIndex)) { yield return null; }
    }

    bool OneSpinRoulette(RouletteBase roulette)
    {
        return roulette.endPosition != (roulette.transform.localPosition =
            Vector3.MoveTowards(roulette.transform.localPosition, roulette.endPosition, rouletteSpeed * Time.deltaTime));
    }

    bool ResultRoulette(RouletteBase roulette, int resultIndex)
    {
        if (resultIndex == 1)
        {
            return roulette.resultPosition1 != (roulette.transform.localPosition =
                Vector3.MoveTowards(roulette.transform.localPosition, roulette.resultPosition1, rouletteSpeed * Time.deltaTime));
        }
        else if (resultIndex == 2)
        {
            return roulette.resultPosition2 != (roulette.transform.localPosition =
                Vector3.MoveTowards(roulette.transform.localPosition, roulette.resultPosition2, rouletteSpeed * Time.deltaTime));
        }
        else
        {
            return false;
        }
    }

    IEnumerator LerpSpinRoulette(RouletteBase roulette, int resultIndex, int spinCount, float spinSeconds) //
    {
        Vector3 startPos = roulette.startPosition;
        Vector3 endPos = startPos + (Vector3.down * 450 * spinCount);

        if (resultIndex == 1)
            endPos += Vector3.down * 150;
        else if (resultIndex == 2)
            endPos += Vector3.down * 300;
        else
            endPos += Vector3.down * 450;

        float curSpinTime = 0;

        while (LerpRoulette(roulette, startPos, endPos, curSpinTime))
        {
            if (roulette.transform.localPosition.y <= -450f)
            {
                curSpinTime = 0;
                roulette.transform.localPosition += Vector3.up * 450f;
                endPos += Vector3.up * 450f;
            }

            curSpinTime += Time.deltaTime / spinSeconds;
            yield return null;
        }
    }

    private bool LerpRoulette(RouletteBase roulette, Vector3 startPos, Vector3 endPos, float curSpinTime)
    {
        return endPos != (roulette.transform.localPosition =
            Vector3.Lerp(startPos, endPos, curSpinTime));
    }

    IEnumerator ReduceSpeedRoulette(int resultIndex0, int resultIndex1, int resultIndex2)
    {
        StartCoroutine(LerpSpinRoulette(roulette0, resultIndex0, Random.Range(4, 6), 1f));
        StartCoroutine(LerpSpinRoulette(roulette1, resultIndex1, Random.Range(5, 7), 1.2f));
        yield return StartCoroutine(LerpSpinRoulette(roulette2, resultIndex2, Random.Range(6, 8), 1.4f));

        battleManager.IsUsingRoulette = false;

        battleManager.battleCanvas.UpdateCharacterAtk();

        if (battleManager.IsAutoBattle)
            battleManager.OnClickAttackButton();
    }
}
