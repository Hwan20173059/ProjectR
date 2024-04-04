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

    public int questId;
    public QuestState currentQuestState;

    private void Start()
    {
        Quest quest = QuestManager.instance.QuestStateCheck(questId);
        print(quest.info.displayName);
        QuestUpdate();
        GetComponentsInChildren<TextMeshProUGUI>()[0].text = quest.info.displayName;
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvent.onQuestStateChange += QuestStateChange;
    }

    public void QuestSelect()
    {
        GameEventManager.instance.questEvent.QuestSelect(questId);
    }

    public void QuestUpdate()
    {
        currentQuestState = QuestManager.instance.QuestStateCheck(questId).state;
        //quest = QuestManager.instance.QuestStateCheck(this);
        //currentQuestState = quest.state;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onQuestStateChange -= QuestStateChange;
    }


    private void QuestStateChange(Quest quest)
    {
        Debug.Log("상태 변경 감지");
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
        }
    }
}
