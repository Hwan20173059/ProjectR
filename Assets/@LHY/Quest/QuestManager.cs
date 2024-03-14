using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
/*
 * QuestManager는 모든 퀘스트를 상태를 전체적으로 관리함.
 * 이벤트 중심
 * Quest는 ID를 중심으로 시작, 퀘스트 이동(다음 스텝), 클리어후 보상 등의 이벤트
 * 플레이어 레벨 등 또한 수신받을 예정
 * QuestStateChange : 퀘스트 변경 이벤트
 */
public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;


    //진행가능 요구사항 현재 레벨만 구현
    private int currentPlayerLevel = 1;//수정필요


    private void Awake()
    {
        questMap = CreatQuestMap();
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvent.onStartQuest += StartQuest;
        GameEventManager.instance.questEvent.onAdvanceQuest += AdvanceQuest;
        GameEventManager.instance.questEvent.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onStartQuest -= StartQuest;
        GameEventManager.instance.questEvent.onAdvanceQuest -= AdvanceQuest;
        GameEventManager.instance.questEvent.onFinishQuest -= FinishQuest;

    }

    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            GameEventManager.instance.questEvent.QuestStateChange(quest);
        }
    }

    private void Update()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.Requirments_Not && CheckRequirements(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.Can_Start);
            }
        }
    }

    // todo : 
    private void StartQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        Debug.Log("퀘스트스타트" + id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.In_Progress);

    }

    public void AdvanceQuest(string id)
    {
        Debug.Log(id);
        Quest quest = GetQuestByID(id);

        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            Debug.Log("다음 진행");
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            Debug.Log("완료가능");
            ChangeQuestState(quest.info.id, QuestState.Can_Finish);
        }
    }
    private void FinishQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        //todo : 보상 지급
        Debug.Log(quest + "퀘스트를 클리어했습니다");
        ChangeQuestState(quest.info.id, QuestState.Finished);
    }

    private Dictionary<string, Quest> CreatQuestMap()
    {
        QuestInfoSO[] allQuest = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuest)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.Log("ID : " + questInfo.id + " 에 Error");
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));

            Debug.Log(questInfo.id + "퀘스트 추가");
        }
        return idToQuestMap;
    }

    //직접 액세스하지 않고 이 메서드를 사용
    private Quest GetQuestByID(string id)
    {
        Quest quest = questMap[id];
        if (quest == null) 
        {
            Debug.Log("questMap을 찾지 못함 " + id);
        }
        return quest;
    }

    //todo : 요구사항 충족 판별 메서드(현재 only 레벨)
    private bool CheckRequirements(Quest quest)
    {
        //레벨을 확인한다.
        if (currentPlayerLevel < quest.info.levelRequirement)
            return false;

        //todo : 전제조건 확인
        //QuestInfo.questPrerequisites

        return true;
    }



    //todo : ChangeQuestState() : 상태 변경 메서드
    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestByID(id);
        quest.state = state;
        GameEventManager.instance.questEvent.QuestStateChange(quest);

    }
}
