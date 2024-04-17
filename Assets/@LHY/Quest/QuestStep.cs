using Assets.PixelFantasy.PixelTileEngine.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestStep : MonoBehaviour
{
    public int questID;

    public int questCurrentValue;
    public int questClearValue;
    public string questType;

    public int questValueID;


    public int GetQuestCurrentValue(int id)
    {
        if (id == questID)
        {
            return questCurrentValue;
        }
        return -1;
    }


    private void OnEnable()
    {
        GameEventManager.instance.battleEvent.onKillMonster += KillMonster;
        GameEventManager.instance.battleEvent.onDungeonClear += DungeonClear;
        GameEventManager.instance.uiEvent.onChangeEquip += ItemEquip;
        GameEventManager.instance.uiEvent.onGacha += Gacha;
    }

    private void OnDisable()
    {
        GameEventManager.instance.battleEvent.onKillMonster -= KillMonster;
        GameEventManager.instance.battleEvent.onDungeonClear -= DungeonClear;
        GameEventManager.instance.uiEvent.onChangeEquip -= ItemEquip;
        GameEventManager.instance.uiEvent.onGacha -= Gacha;
    }

    public void Gacha()
    {
        if (questType == "Gacha")
            questCurrentValue++;
        if (questCurrentValue < questClearValue)
        {
            //
        }
        else
        {
            GameEventManager.instance.questEvent.AdvanceQuest(questID);
        }
    }

    public void KillMonster(int id)
    {
        if (questType == "KillMonster" && id == questValueID)
        {
            questCurrentValue++;
            QuestManager.instance.GetQuestByID(questID).info.questCurrentValue = questCurrentValue;
        }
        if (questCurrentValue < questClearValue)
        {
            //
        }
        else
        {
            GameEventManager.instance.questEvent.AdvanceQuest(questID);
        }
    }
    public void DungeonClear(int id)
    {
        if (questType == "DungeonClear")
        {
            questCurrentValue++;
            QuestManager.instance.GetQuestByID(questID).info.questCurrentValue = questCurrentValue;
        }
        if (questCurrentValue < questClearValue)
        {
            //
        }
        else
        {
            GameEventManager.instance.questEvent.AdvanceQuest(questID);
        }
    }

    private void Update()
    {
        if (questType == "CollectConsumeItem")
        {
            foreach (ConsumeItem item in ItemManager.Instance.cInventory)
            {
                if (QuestManager.instance.GetQuestByID(questID).info.questValueID == item.data.id)
                {
                    questCurrentValue = item.count;
                    QuestManager.instance.GetQuestByID(questID).info.questCurrentValue = questCurrentValue;
                }
            }
        }
        if (questCurrentValue < questClearValue)
        {
            // 재료를 모으고 Can_Finish 상태에서 만약 재료가 다시 부족해졌다면 다시 In_Progress상태로 만들어야함.
            // 재료를 퀘스트 이외에서 줄어드는 곳이 따로 없다면 할 필요 없음.
        }
        else
        {
            GameEventManager.instance.questEvent.AdvanceQuest(questID);
        }
    }
    public void CollectConsumeItem(int id)
    {
        if (questCurrentValue < questClearValue)
        {
            // 재료를 모으고 Can_Finish 상태에서 만약 재료가 다시 부족해졌다면 다시 In_Progress상태로 만들어야함.
            // 재료를 퀘스트 이외에서 줄어드는 곳이 따로 없다면 할 필요 없음.
        }
        else
        {
            GameEventManager.instance.questEvent.AdvanceQuest(questID);
        }
    }

    public void ItemEquip(int id)
    {
        if (questType == "ItemEquip")
            questCurrentValue++;
        if (questCurrentValue < questClearValue)
        {
            //
        }
        else
        {
            GameEventManager.instance.questEvent.AdvanceQuest(questID);
        }
    }
    public void FieldClear()
    {
        if (questType == "FieldClear")
            questCurrentValue++;
        if (questCurrentValue < questClearValue)
        {
            //
        }
        else
        {
            GameEventManager.instance.questEvent.AdvanceQuest(questID);
        }
    }
}
