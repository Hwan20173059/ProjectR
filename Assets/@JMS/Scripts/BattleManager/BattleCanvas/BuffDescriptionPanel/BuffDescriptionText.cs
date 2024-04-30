using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffDescriptionText : MonoBehaviour
{
    TextMeshProUGUI buffDescriptionText;

    public void Init()
    {
        buffDescriptionText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateBuffText(Buff buff)
    {
        if (buff.type == BuffType.SPD)
        {
            buffDescriptionText.text = $"{buff.buffName}\n���� Ÿ�� : {buff.type}\nȿ�� : +{buff.value}%\n���� �� : {buff.turnCount}";
        }
        else
        {
            buffDescriptionText.text = $"{buff.buffName}\n���� Ÿ�� : {buff.type}\nȿ�� : +{buff.value}\n���� �� : {buff.turnCount}";
        }
    }
}
