using UnityEngine;
using System;
using Unity.VisualScripting.FullSerializer;


public class QuestEvent
{
    public event Action<int> onStartQuest;
    public void StartQuest(int id)
    {
        if (onStartQuest != null)
        {
            Debug.Log("QuestStart");
            onStartQuest(id);
        }
    }
    public event Action<int> onAdvanceQuest;

    public void AdvanceQuest(int id)
    {
        if (onAdvanceQuest != null)
        {
            onAdvanceQuest(id);
        }
    }
    public event Action<int> onFinishQuest;

    public void FinishQuest(int id)
    {
        if (onFinishQuest != null)
        {
            onFinishQuest(id);
        }
    }
    public event Action<Quest> onQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        if (onQuestStateChange != null)
        {
            onQuestStateChange(quest);
        }
    }

    //진행상태
    public event Action<int, QuestStepState> onQuestStepStateChange;
    public void QuestStepStateChange(int id, QuestStepState questStepState)
    {
        if (onQuestStepStateChange != null)
        {
            onQuestStepStateChange(id, questStepState);
        }
    }

    //todo : 다른 event로 분리
    //임시
    public event Action onDungeonClear;
    public void DungeonClear()
    {
        
        if (onDungeonClear != null)
        {
            onDungeonClear();
        }
    }
    //
    public event Action<int> onSubmitPressed;
    public void SubmitPressed(int id)
    {
        
        if (onSubmitPressed != null)
        {
            onSubmitPressed(id);
        }
    }

    public event Action<int> onQuestSelect;
    public void QuestSelect(int id)
    {

        if (onQuestSelect != null)
        {
            onQuestSelect(id);
        }
    }
}
