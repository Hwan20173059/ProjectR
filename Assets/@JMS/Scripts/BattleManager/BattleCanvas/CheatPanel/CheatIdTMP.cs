using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheatIdTMP : MonoBehaviour
{
    TextMeshProUGUI TMP;
    public string text { get { return TMP.text; } set { TMP.text = value; } }

    public void Init()
    {
        TMP = GetComponentInChildren<TextMeshProUGUI>();
    }
}
