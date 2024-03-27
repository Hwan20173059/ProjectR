using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulettePanel : MonoBehaviour
{
    Roulette1 roulette1;
    Roulette2 roulette2;
    Roulette3 roulette3;
    VoidRoulette voidRoulette;

    public void Init()
    {
        roulette1 = GetComponentInChildren<Roulette1>();
        roulette2 = GetComponentInChildren<Roulette2>();
        roulette3 = GetComponentInChildren<Roulette3>();

        voidRoulette = GetComponentInChildren<VoidRoulette>();
    }
    public void SetRoulette(BattleManager battleManager, int i)
    {
        switch (i)
        {
            case 0: roulette1.image.sprite = battleManager.rouletteEquip[i].equipSprite; break;
            case 1: roulette2.image.sprite = battleManager.rouletteEquip[i].equipSprite; break;
            case 2: roulette3.image.sprite = battleManager.rouletteEquip[i].equipSprite; break;
        }
    }
    public void ClearRoulette()
    {
        roulette1.image.sprite = voidRoulette.image.sprite;
        roulette2.image.sprite = voidRoulette.image.sprite;
        roulette3.image.sprite = voidRoulette.image.sprite;
    }
}
