using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleText : MonoBehaviour
{
    TextMeshProUGUI battleText;

    public void Init()
    {
        battleText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateBattleText(string text)
    {
        battleText.text = $"{text}";
    }
}
