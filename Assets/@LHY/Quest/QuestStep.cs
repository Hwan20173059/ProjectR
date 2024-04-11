using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    public int questID;

    public int questCurrentValue;
    public int questClearValue;
    public string questType;



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
        GameEventManager.instance.questEvent.onDungeonClear += DungeonClear;
        GameEventManager.instance.battleEvent.onKillSlime += KillSlime;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onDungeonClear -= DungeonClear;
        GameEventManager.instance.battleEvent.onKillSlime -= KillSlime;
        //GameEventManager.instance.questEvent.onFinishQuest -= FinishQuest;
    }

    private void Awake()
    {
        init();
    }

    private void init()
    {
        //todo : 진행중인 QuestData 불러오기
    }

    public void KillSlime()
    {
        if (questType == "KillSlime")
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

    public void DungeonClear()
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

    public void ItemEquip()
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

    public void InitializeQuestStep(int questId, string questStepState)
    {
        this.questID = questId;
        if (questStepState != null && questStepState != "")
        {
            //SetQuestStepState(questStepState);
        }
    }
    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            GameEventManager.instance.questEvent.AdvanceQuest(questID);
            Destroy(gameObject);
        }
    }
    protected void ChangeState(string newState, string newStatus)
    {
        Debug.Log("changeState");
        GameEventManager.instance.questEvent.QuestStepStateChange(questID, new QuestStepState(newState, newStatus));
    }
    
    //protected abstract void SetQuestStepState(string state);

}
