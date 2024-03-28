using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterStateText : MonoBehaviour
{
    TextMeshProUGUI monsterStateText;
    public string text { get { return monsterStateText.text; } set { monsterStateText.text = value; } }

    private void Awake()
    {
        monsterStateText = GetComponent<TextMeshProUGUI>();
    }
}
