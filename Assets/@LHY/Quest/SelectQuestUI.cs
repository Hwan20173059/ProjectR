using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class SelectQuestUI : MonoBehaviour
{
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questSubmitButtonText;
    public TextMeshProUGUI selectQuestButton;

    
    public QuestSlot selectQuestSlot;
    public int selectQuestID;
    public QuestState selectQuestState;

    QuestManager questManager;

    public QuestInfoSO questInfoSO;
    private void Awake()
    {
        questManager = GetComponent<QuestManager>();
    }
    private void OnEnable()
    {
        GameEventManager.instance.questEvent.onQuestSelect += QuestSelect;
        GameEventManager.instance.questEvent.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onQuestSelect -= QuestSelect;
        GameEventManager.instance.questEvent.onSubmitPressed += SubmitPressed;
    }


    public void SubmitOnClick()
    {
        Debug.Log("Select");
        GameEventManager.instance.questEvent.SubmitPressed(selectQuestID);
        SetQuest(selectQuestID);
    }

    public void QuestSelect(int id)
    {
        selectQuestID = id;
        SetQuest(selectQuestID);
        infoUpdate();
    }


    public void SetQuest(int id)
    {
        selectQuestState = QuestManager.instance.GetQuestByID(id).state;
    }

    private void Update()
    {
        
    }

    public void infoUpdate()
    {
        Quest quest = QuestManager.instance.GetQuestByID(selectQuestID);
        questName.text = quest.info.displayName;
        questDescription.text = quest.info.description + '\n' +
            "\n���൵ : " + quest.info.questCurrentValue + "/" + quest.info.questClearValue;
        if (quest.state == QuestState.Requirments_Not)
        {
            selectQuestButton.text = "�䱸���� ������";
            selectQuestButton.color = Color.red;
        }
        if (quest.state == QuestState.Can_Start)
        {
            selectQuestButton.text = "����";
            selectQuestButton.color = Color.black;
        }
        if (quest.state == QuestState.In_Progress)
        {
            selectQuestButton.text = "������";
            selectQuestButton.color = Color.gray;
        }
        if (quest.state == QuestState.Can_Finish)
        {
            selectQuestButton.text = "�Ϸ� ����";
            selectQuestButton.color = Color.green;
        }
        if (quest.state == QuestState.Finished)
        {
            selectQuestButton.text = "����Ʈ �Ϸ�";
            selectQuestButton.color = Color.blue;
        }
    }
    public void SubmitPressed(int id)
    {
        Debug.Log(selectQuestState);
        if (id !=  selectQuestID)
        {
            Debug.Log("����Ʈ ���ÿ� ������ �ֽ��ϴ�.");
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
