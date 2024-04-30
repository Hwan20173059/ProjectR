using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class CheatPanel : MonoBehaviour
{
    CheatButton cheatButton;
    IdMinusButton idMinusButton;
    IdPlusButton idPlusButton;
    CheatIdTMP cheatIdTMP;

    public string text { get { return cheatIdTMP.text; } set { cheatIdTMP.text = value; } }
    public int cheatId = 0;

    public void Init(BattleManager battleManager)
    {
        cheatButton = GetComponentInChildren<CheatButton>();
        cheatIdTMP = GetComponentInChildren<CheatIdTMP>();
        idMinusButton = GetComponentInChildren<IdMinusButton>();
        idPlusButton = GetComponentInChildren<IdPlusButton>();

        cheatButton.Init(battleManager, this);
        cheatIdTMP.Init();
        idMinusButton.Init(this);
        idPlusButton.Init(this);

        gameObject.SetActive(false);
    }
}
