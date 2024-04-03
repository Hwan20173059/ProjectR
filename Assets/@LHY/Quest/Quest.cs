using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Quest
{
    //public QuestInfoSO info;
    public QuestState state;

    public QuestData info;
    public QuestInfo questinfo;
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

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        Debug.Log("Asdf");
        GameObject questStepPrefab = GetCurrentQuestStepPrefeb();
        QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
        if (questStepPrefab != null)
        {
            //QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            //questStep.InitializeQuestStep(info.id, questStepStates[currentQuestStepIndex].state);
        }
    }
    private GameObject GetCurrentQuestStepPrefeb()
    {
        //초기화 이후 다음 퀘스트 진행(현재 연결 퀘스트 구현 X)
        GameObject questStepPrefeb = new GameObject();

        return questStepPrefeb;
    }

    
}


//todo : 분리
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
    public int needLevel;
    public int needGold;
    public string questStep;
    public int goldReward;
    public int expReward;
    public int consumeRewardID;
    public int equipRewardID;
}