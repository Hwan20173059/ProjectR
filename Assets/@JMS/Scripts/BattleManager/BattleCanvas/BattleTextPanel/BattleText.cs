using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleText : MonoBehaviour
{
    TextMeshProUGUI battleText;

    public string text { get {  return battleText.text; } set { battleText.text = value; } }

    private void Awake()
    {
        battleText = GetComponent<TextMeshProUGUI>();
    }
}
