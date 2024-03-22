using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Quest
{
    public QuestInfoSO info;
    public QuestState state;
    private int currentQuestStepIndex;
    private QuestStepState[] questStepStates;



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

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    // 다음 step이 없는 경우에 대비
    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefebs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefeb();
        if (questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform)
                .GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
        }
    }
    private GameObject GetCurrentQuestStepPrefeb()
    {
        //초기화 이후 다음 퀘스트 진행(현재 연결 퀘스트 구현 X)
        GameObject questStepPrefeb = null;
        if (CurrentStepExists())
        {
            questStepPrefeb = info.questStepPrefebs[currentQuestStepIndex];
        }
        else
        {
            Debug.Log("퀘스트 아이디가 " + info.id + "의 "+ currentQuestStepIndex + " 번째 프리팹 확인해");
        }
        return questStepPrefeb;
    }

    
}
