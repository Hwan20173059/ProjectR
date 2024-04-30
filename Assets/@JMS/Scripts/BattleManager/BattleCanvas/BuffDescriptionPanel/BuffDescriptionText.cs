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
            buffDescriptionText.text = $"{buff.buffName}\n버프 타입 : {buff.type}\n효과 : +{buff.value}%\n남은 턴 : {buff.turnCount}";
        }
        else
        {
            buffDescriptionText.text = $"{buff.buffName}\n버프 타입 : {buff.type}\n효과 : +{buff.value}\n남은 턴 : {buff.turnCount}";
        }
    }
}
