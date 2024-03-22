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

    // ���� step�� ���� ��쿡 ���
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
        //�ʱ�ȭ ���� ���� ����Ʈ ����(���� ���� ����Ʈ ���� X)
        GameObject questStepPrefeb = null;
        if (CurrentStepExists())
        {
            questStepPrefeb = info.questStepPrefebs[currentQuestStepIndex];
        }
        else
        {
            Debug.Log("����Ʈ ���̵� " + info.id + "�� "+ currentQuestStepIndex + " ��° ������ Ȯ����");
        }
        return questStepPrefeb;
    }

    
}
