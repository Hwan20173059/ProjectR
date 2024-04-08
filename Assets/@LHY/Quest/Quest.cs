using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Quest
{
    //public QuestInfoSO info;
    public QuestState state;
    public QuestData info;
    public QuestStepState questStepState;

    //public QuestInfo questinfo;
    /*
    public Quest(QuestInfoSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.Requirments_Not;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[info.questStepPrefebs.Length];
        for (int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
    }
    */

    
    public Quest(QuestData questData)
    {
        this.info = questData;
        this.state = QuestState.Requirments_Not;
    }

    //Load
    public Quest(QuestData questData, QuestState questState, QuestStepState questStepState)
    {
        this.info = questData;
        this.state = questState;

        this.questStepState = questStepState;
    }
    /*
    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }
    */
    /*
    public bool CurrentStepExists()
    {
        //return (currentQuestStepIndex < info.questStepPrefebs.Length);
        return true;
    }
    */

    public void InstantiateCurrentQuestStep(Transform parentTransform, int id)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefeb();
        QuestStep queststep = Object.Instantiate(questStepPrefab, parentTransform).AddComponent<QuestStep>();
        queststep.questID = id;
        queststep.questClearValue = QuestManager.instance.GetQuestByID(id).info.questClearValue;
        queststep.questType = QuestManager.instance.GetQuestByID(id).info.questType;
    }

    private GameObject GetCurrentQuestStepPrefeb()
    {
        GameObject questStepPrefeb = new GameObject();

        return questStepPrefeb;
    }
}


//todo : ºÐ¸®
[System.Serializable]
public class AllData
{
    public QuestData[] quest;
}

[System.Serializable]
public class QuestData
{
    public int id;
    public string displayName;
    public string description;
    public string questType;
    public int questCurrentValue;
    public int questClearValue;
    public int needLevel;
    public int needGold;
    public QuestState questState;
    public int goldReward;
    public int expReward;
    public int consumeRewardID1;
    public int equipRewardID;
}