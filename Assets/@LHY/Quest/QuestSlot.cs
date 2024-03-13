using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSlot : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;

    private string questId;
    public QuestState currentQuestState;

    private void Awake()
    {
        questId = questInfoForPoint.id;
        Debug.Log(questId);
        Debug.Log(GameEventManager.instance.questEvent);
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvent.onQuestStateChange += QuestStateChange;
        GameEventManager.instance.questEvent.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onQuestStateChange -= QuestStateChange;
        GameEventManager.instance.questEvent.onSubmitPressed += SubmitPressed;
    }

    private void Start()
    {
        Debug.Log(questId);
    }
    //수락 버튼 press
    public void SubmitPressed()
    {
        Debug.Log(currentQuestState);
        if (currentQuestState.Equals(QuestState.Can_Start))
        {
            Debug.Log(questId + "퀘스트를 시작합니다.");
            GameEventManager.instance.questEvent.StartQuest(questId);
        }
        else if (currentQuestState.Equals(QuestState.Can_Finish))
        {
            Debug.Log(questId + "퀘스트를 완료했습니다.");
            GameEventManager.instance.questEvent.FinishQuest(questId);
        }
    }
    private void QuestStateChange(Quest quest)
    {
        Debug.Log(quest.state);
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
        }
    }
}
