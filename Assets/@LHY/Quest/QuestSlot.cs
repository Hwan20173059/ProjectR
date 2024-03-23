using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class QuestSlot : MonoBehaviour
{
    [Header("Quest")]
    //[SerializeField] private QuestInfoSO questInfoForPoint;
    public QuestInfoSO questInfoForPoint;

    public string questId;
    public QuestState currentQuestState;

    public int goldReward;
    public int expReward;
    public int levelRequirement;
    public int goldRequirement;

    private void Awake()
    {
        questId = questInfoForPoint.id;
        levelRequirement = questInfoForPoint.levelRequirement;
        //goldRequirement = questInfoForPoint.goldRequirement;
        //todo : 퀘스트에 gold 요구 추가 예정

        Debug.Log(questId);
        QuestUpdate();
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvent.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onQuestStateChange -= QuestStateChange;
    }

    public void QuestUpdate()
    {
        Quest quest;
        quest = QuestManager.instance.QuestStateCheck(this);
        Debug.Log(quest.state);
        currentQuestState = quest.state;
        goldReward = quest.info.goldReward;
        expReward = quest.info.expReward;
    }


    private void Start()
    {

    }

    private void QuestStateChange(Quest quest)
    {
        Debug.Log("상태 변경 감지");
        if (quest.info.id.Equals(questInfoForPoint.id))
        {
            currentQuestState = quest.state;
        }
    }
}
