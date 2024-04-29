using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffDescriptionText : MonoBehaviour
{
    TextMeshProUGUI buffDescriptionText;
    public string text { get { return buffDescriptionText.text; } set { buffDescriptionText.text = value; } }

    private void Awake()
    {
        buffDescriptionText = GetComponent<TextMeshProUGUI>();
    }
}
