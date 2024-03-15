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
        currentQuestState = QuestManager.instance.currentState;
    }
    //���� ��ư press
    public void SubmitPressed(string selectQuestId)
    {
        //questId = selectQuestId;
        
        if (questInfoForPoint.id != selectQuestId)
        {
            Debug.Log("����");
            return;
        }
        questId = selectQuestId;
        Debug.Log(currentQuestState + questId);
        if (currentQuestState.Equals(QuestState.Can_Start))
        {
            Debug.Log(questId + "����Ʈ�� �����մϴ�.");
            GameEventManager.instance.questEvent.StartQuest(questId);
        }
        else if (currentQuestState.Equals(QuestState.Can_Finish))
        {
            Debug.Log(questId + "����Ʈ�� �Ϸ��߽��ϴ�.");
            currentQuestState = QuestState.Finished;
            GameEventManager.instance.questEvent.FinishQuest(questId);
        }
        infoUpdate();
    }
    private void QuestStateChange(Quest quest)
    {
        Debug.Log("���� ���� ����");
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
            QuestSlottext.color = Color.gray;
        if (currentQuestState == QuestState.Can_Finish)
            QuestSlottext.color = Color.green;
        if (currentQuestState == QuestState.Finished)
            QuestSlottext.color = Color.blue;
        questDescription.text = questInfoForPoint.questDescription;
        questSubmitButtonText.text = currentQuestState.ToString();
    }
}
