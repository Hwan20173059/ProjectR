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

    public GameObject selectQuest;

    public SelectQuestUI selectQuestUI;



    private void Awake()
    {
        questId = questInfoForPoint.id;
        Debug.Log(questId);
        QuestUpdate();
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvent.onQuestStateChange += QuestStateChange;
    }

    public void QuestUpdate()
    {
        Quest quest;
        quest = QuestManager.instance.QuestStateCheck(this);
        Debug.Log(quest.state);
        currentQuestState = quest.state;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onQuestStateChange -= QuestStateChange;
    }

    private void Start()
    {

    }

    //현재 진행중인지 확인하는 메서드


    private void QuestStateChange(Quest quest)
    {
        Debug.Log("상태 변경 감지");
        if (quest.info.id.Equals(questInfoForPoint.id))
        {
            currentQuestState = quest.state;
        }
    }
}
