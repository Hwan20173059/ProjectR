using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDescriptionPanel : MonoBehaviour
{
    BuffDescriptionText buffDescriptionText;

    public void Init()
    {
        buffDescriptionText = GetComponentInChildren<BuffDescriptionText>();

        gameObject.SetActive(false);
    }

    public void SetBuffText(Buff buff)
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
