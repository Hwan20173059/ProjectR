using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//퀘스트 step에 이용, 단일퀘스트용 
public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questID;
    private int stepIndex;


    public void InitializeQuestStep(string questId, int stepIndex, string questStepState)
    {
        this.questID = questId;
        this.stepIndex = stepIndex;
        if (questStepState != null && questStepState != "")
        {
            SetQuestStepState(questStepState);
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
        GameEventManager.instance.questEvent.QuestStepStateChange(
            questID,
            stepIndex,
            new QuestStepState(newState, newStatus)
            );
    }
    
    protected abstract void SetQuestStepState(string state);

}
