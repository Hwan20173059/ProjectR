using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class SelectQuestUI : MonoBehaviour
{

    public SelectQuestUI selectQuestUI;
    public GameObject selectQuest;
    //public TextMeshProUGUI QuestSlottext;
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questSubmitButtonText;
    public TextMeshProUGUI selectQuestButton;

    
    public QuestSlot selectQuestSlot;
    public string selectQuestID;
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

    public void QuestSelect(QuestSlot questSlot)
    {
        Debug.Log("선택");
        selectQuestSlot = questSlot;
        SetQuest(questSlot.questId);
        infoUpdate();
    }


    public void SetQuest(string id)
    {
        selectQuestID = id;
        selectQuestState = selectQuestSlot.currentQuestState;
        //이거는 퀘스트가 진행중일때 받아와야함.
        //selectQuestState = QuestManager.instance.GetComponentInChildren<ClearDungeonQuestStep>().currentState;


        //print(QuestManager.instance.GetComponentInChildren<ClearDungeonQuestStep>().dungeonCleared);
    }

    private void Update()
    {
        //print(QuestManager.instance.GetComponentInChildren<ClearDungeonQuestStep>().dungeonCleared);
    }
    public void infoUpdate()
    {
        Debug.Log(selectQuestSlot.questInfoForPoint.displayName);
        questName.text = selectQuestSlot.questInfoForPoint.displayName;
        questDescription.text = selectQuestSlot.questInfoForPoint.questDescription;
        selectQuestButton.text = selectQuestSlot.currentQuestState.ToString();
    }
    public void SetSelectQuestPanel()
    {
        selectQuest.SetActive(!selectQuest.activeSelf);
    }
    public void SubmitPressed(string id)
    {
        Debug.Log(selectQuestState);
        if (id !=  selectQuestID)
        {
            Debug.Log("퀘스트 선택에 오류가 있습니다.");
        }

        if (selectQuestSlot.questInfoForPoint.id != selectQuestID)
        {
            Debug.Log("ㅇㅇ");
            return;
        }
        if (selectQuestState.Equals(QuestState.Can_Start))
        {
            Debug.Log(GameEventManager.instance.questEvent);
            GameEventManager.instance.questEvent.StartQuest(selectQuestID);
        }
        else if (selectQuestState.Equals(QuestState.Can_Finish))
        {
            Debug.Log(selectQuestID + "퀘스트를 완료했습니다.");
            selectQuestState = QuestState.Finished;
            GameEventManager.instance.questEvent.FinishQuest(selectQuestID);
        }
        infoUpdate();
    }

}
