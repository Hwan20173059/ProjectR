using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTextPanel : MonoBehaviour
{
    BattleText battleText;
    public void Init()
    {
        battleText = GetComponentInChildren<BattleText>();
    }

    public void UpdateBattleState(string text)
    {
        battleText.text = $"{text}";
    }
}
