using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageTMP : MonoBehaviour
{
    TextMeshProUGUI stageText;

    public string text { get { return stageText.text; } set { stageText.text = value; } }

    private void Awake()
    {
        stageText = GetComponent<TextMeshProUGUI>();
    }
}
