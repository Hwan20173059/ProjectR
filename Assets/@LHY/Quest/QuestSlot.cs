using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class QuestSlot : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;

    public string questId;
    public QuestState currentQuestState;

    public GameObject selectQuest;

    public SelectQuestUI selectQuestUI;

    public TextMeshProUGUI QuestSlottext;
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questSubmitButtonText;

    private void Awake()
    {
        questId = questInfoForPoint.id;
        Debug.Log(questId);
        QuestSlottext.text = questInfoForPoint.displayName;
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvent.onQuestStateChange += QuestStateChange;
        GameEventManager.instance.questEvent.onSubmitPressed += SubmitPressed;
    }



    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onQuestStateChange -= QuestStateChange;
        GameEventManager.instance.questEvent.onSubmitPressed -= SubmitPressed;
    }

    private void Start()
    {
        Debug.Log(questId);
    }
    //수락 버튼 press
    public void SubmitPressed(string selectQuestId)
    {
        //questId = selectQuestId;
        
        if (questInfoForPoint.id != selectQuestId)
        {
            Debug.Log("ㅇㅇ");
            return;
        }
        questId = selectQuestId;
        Debug.Log(currentQuestState + questId);
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
        infoUpdate();
    }
    private void QuestStateChange(Quest quest)
    {
        Debug.Log("상태 변경 감지");
        infoUpdate();
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
        }
    }

    public void QuestSelect()
    {
        questId = questInfoForPoint.id;
        selectQuestUI.setQuestID(questId);
        SetSelectQuestPanel();
        //questUIControler.init(questInfoForPoint.displayName, questInfoForPoint.questDescription);
        infoUpdate();
    }

    public void SetSelectQuestPanel()
    {
        selectQuest.SetActive(!selectQuest.activeSelf);
    }

    public void infoUpdate()
    {
        QuestSlottext.text = questInfoForPoint.displayName;
        questName.text = questInfoForPoint.displayName;
        if (currentQuestState == QuestState.In_Progress)
            QuestSlottext.color = Color.red;
        if (currentQuestState == QuestState.Finished)
            QuestSlottext.color = Color.green;
        questDescription.text = questInfoForPoint.questDescription;
        questSubmitButtonText.text = currentQuestState.ToString();
    }
}
