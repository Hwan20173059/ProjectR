using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvent
{
    public event Action<string> onSubmitPressed;
    public void SubmitPressed(string id)
    {

        if (onSubmitPressed != null)
        {
            onSubmitPressed(id);
        }
    }

    public event Action<QuestSlot> onQuestSelect;
    public void QuestSelect(QuestSlot questslot)
    {

        if (onQuestSelect != null)
        {
            onQuestSelect(questslot);
        }
    }

    public event Action onUIClick;
    public void UIClick()
    {
        if (onUIClick != null)
        {
            onUIClick();
        }
    }

    public event Action<int> onChangeEquip;
    public void ChangeEquip(int id)
    {
        if (onChangeEquip != null)
        {
            onChangeEquip(id);
        }
    }

    public event Action onGacha;
    public void Gacha()
    {
        if (onGacha != null)
        {
            onGacha();
        }
    }
}
