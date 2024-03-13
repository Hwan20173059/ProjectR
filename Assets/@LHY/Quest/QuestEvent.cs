using UnityEngine;
using System;


public class QuestEvent
{
    public event Action<string> onStartQuest;
    //QuestID�� �ԷµǾ�� ��

    public void StartQuest(string id)
    {
        if (onStartQuest != null)
        {
            onStartQuest(id);
        }
    }
    public event Action<string> onAdvanceQuest;

    public void AdvanceQuest(string id)
    {
        if (onAdvanceQuest != null)
        {
            onAdvanceQuest(id);
        }
    }
    public event Action<string> onFinishQuest;

    public void FinishQuest(string id)
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

    //�������
    public event Action<string, int, QuestStepState> onQuestStepStateChange;
    public void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        if (onQuestStepStateChange != null)
        {
            onQuestStepStateChange(id, stepIndex, questStepState);
        }
    }

    //todo : �ٸ� event�� �и�
    //�ӽ�
    public event Action onDungeonClear;
    public void DungeonClear()
    {
        
        if (onDungeonClear != null)
        {
            onDungeonClear();
        }
    }
    //
    public event Action onSubmitPressed;
    public void SubmitPressed()
    {
        
        if (onSubmitPressed != null)
        {
            onSubmitPressed();
        }
    }
}
