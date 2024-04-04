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


    private void OnEnable()
    {
        if (questType == "DungeonClear")
        {
            GameEventManager.instance.questEvent.onDungeonClear += DungeonClear;
        }
        GameEventManager.instance.questEvent.onDungeonClear += DungeonClear;
        GameEventManager.instance.battleEvent.onKillSlime += KillSlime;
        if (questType == "KillSlime")
        {
            GameEventManager.instance.battleEvent.onKillSlime += KillSlime;
        }
        //GameEventManager.instance.questEvent.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onDungeonClear -= DungeonClear;
        GameEventManager.instance.battleEvent.onKillSlime -= KillSlime;
        //GameEventManager.instance.questEvent.onFinishQuest -= FinishQuest;
    }

    public void KillSlime()
    {
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
