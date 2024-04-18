using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

    private string desc;
    private void Start()
    {
        Quest quest = QuestManager.instance.QuestStateCheck(questId);
        QuestUpdate();
        if (quest.info.repeatable == "반복")
            desc = "[반복]";
        desc += quest.info.displayName;
        GetComponentsInChildren<TextMeshProUGUI>()[0].text = desc;
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
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
        }
    }
}
