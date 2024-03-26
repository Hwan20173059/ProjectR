using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class SelectQuestUI : MonoBehaviour
{
    [Header("Quest")]
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questSubmitButtonText;
    public TextMeshProUGUI selectQuestButton;
    public TextMeshProUGUI reward;
    public TextMeshProUGUI Requirement;

    public QuestSlot selectQuestSlot;
    public string selectQuestID;
    public QuestState selectQuestState;


    public QuestInfoSO questInfoSO;

    GameEventManager gameEventManager;
    private void Awake()
    {
        gameEventManager = GameEventManager.instance;
    }
    private void OnEnable()
    {
        gameEventManager.uiEvent.onQuestSelect += QuestSelect;
        gameEventManager.uiEvent.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        GameEventManager.instance.uiEvent.onQuestSelect -= QuestSelect;
        GameEventManager.instance.uiEvent.onSubmitPressed += SubmitPressed;
    }


    public void SubmitOnClick()
    {
        Debug.Log("Select");
        GameEventManager.instance.uiEvent.SubmitPressed(selectQuestID);
        SetQuest(selectQuestID);
    }

    public void QuestSelect(QuestSlot questSlot)
    {
        Debug.Log("����");
        selectQuestSlot = questSlot;
        SetQuest(questSlot.questId);
        infoUpdate();
    }


    public void SetQuest(string id)
    {
        selectQuestID = id;
        selectQuestState = selectQuestSlot.currentQuestState;
        
    }

    private void Update()
    {
        
    }
    public void infoUpdate()
    {
        questName.text = selectQuestSlot.questInfoForPoint.displayName;
        questDescription.text = selectQuestSlot.questInfoForPoint.questDescription;
        reward.text = "����\n" + "����ġ : " + selectQuestSlot.expReward.ToString() + "\n��� : " + selectQuestSlot.goldReward.ToString();
        Requirement.text = "�䱸����\n���� : " + selectQuestSlot.levelRequirement.ToString();
        if (selectQuestSlot.currentQuestState == QuestState.Requirments_Not)
        {
            selectQuestButton.text = "�䱸���� ������";
            selectQuestButton.color = Color.red;
        }
        if (selectQuestSlot.currentQuestState == QuestState.Can_Start)
        {
            selectQuestButton.text = "����";
            selectQuestButton.color = Color.black;
        }
        if (selectQuestSlot.currentQuestState == QuestState.In_Progress)
        {
            selectQuestButton.text = "������";
            selectQuestButton.color = Color.gray;
        }
        if (selectQuestSlot.currentQuestState == QuestState.Can_Finish)
        {
            selectQuestButton.text = "�Ϸ� ����";
            selectQuestButton.color = Color.green;
        }
        if (selectQuestSlot.currentQuestState == QuestState.Finished)
        {
            selectQuestButton.text = "����Ʈ �Ϸ�";
            selectQuestButton.color = Color.blue;
        }
    }

    public void SubmitPressed(string id)
    {
        Debug.Log(selectQuestState);
        if (id !=  selectQuestID)
        {
            Debug.Log("����Ʈ ���ÿ� ������ �ֽ��ϴ�.");
        }

        if (selectQuestSlot.questInfoForPoint.id != selectQuestID)
        {
            Debug.Log("����");
            return;
        }
        if (selectQuestState.Equals(QuestState.Can_Start))
        {
            Debug.Log(GameEventManager.instance.questEvent);
            GameEventManager.instance.questEvent.StartQuest(selectQuestID);
        }
        else if (selectQuestState.Equals(QuestState.Can_Finish))
        {
            Debug.Log(selectQuestID + "����Ʈ�� �Ϸ��߽��ϴ�.");
            selectQuestState = QuestState.Finished;
            GameEventManager.instance.questEvent.FinishQuest(selectQuestID);
        }
        infoUpdate();
    }

}
