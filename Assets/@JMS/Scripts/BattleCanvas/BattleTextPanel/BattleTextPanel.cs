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

    public void UpdateBattleText(string text)
    {
        battleText.text = $"{text}";
    }
}
