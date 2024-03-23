using UnityEngine;
using System;
using Unity.VisualScripting.FullSerializer;


public class QuestEvent
{
    public event Action<string> onStartQuest;
    public void StartQuest(string id)
    {
        if (onStartQuest != null)
        {
            Debug.Log("QuestStart");
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

    //진행상태
    public event Action<string, int, QuestStepState> onQuestStepStateChange;
    public void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        if (onQuestStepStateChange != null)
        {
            onQuestStepStateChange(id, stepIndex, questStepState);
        }
    }






    //todo : DungeonEvent등으로 분리
    public event Action onDungeonClear;
    public void DungeonClear()
    {
        
        if (onDungeonClear != null)
        {
            onDungeonClear();
        }
    }

    public event Action onKillSlime;
    public void KillSlime()
    {
        if (onKillSlime != null)
        {
            onKillSlime();
        }
    }




    //todo : UIEvent 등으로 분리
    public event Action<string> onSubmitPressed;
    public void SubmitPressed(string id)
    {
        
        if (onSubmitPressed != null)
        {
            onSubmitPressed(id);
        }
    }

    public event Action<QuestSlot> onQuestSelect;
    public void QuestSelect(QuestSlot questslot)
    {

        if (onQuestSelect != null)
        {
            onQuestSelect(questslot);
        }
    }
}
