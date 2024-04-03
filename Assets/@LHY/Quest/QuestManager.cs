using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;
/*
 * QuestManager는 모든 퀘스트를 상태를 전체적으로 관리함.
 * 이벤트 중심
 * Quest는 ID를 중심으로 시작, 퀘스트 이동(다음 스텝), 클리어후 보상 등의 이벤트
 * 플레이어 레벨 등 또한 수신받을 예정
 * QuestStateChange : 퀘스트 변경 이벤트
 */
public class QuestManager : MonoBehaviour
{
    private static Dictionary<string, Quest> questMap;

    public static Dictionary<int, Quest> NewquestMap;

    //진행가능 요구사항 현재 레벨만 구현
    private int currentPlayerLevel = 1;//수정필요

    public static QuestManager instance;

    public int i = 1;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        //questMap = CreatQuestMap();
        NewquestMap = CreatQuestMaps();
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

    }

    private void Update()
    {
        foreach (Quest quest in NewquestMap.Values)
        {
            if (quest.state == QuestState.Requirments_Not && CheckRequirements(quest))
            {
                GameEventManager.instance.questEvent.QuestStateChange(quest);
                ChangeQuestState(quest.info.id, QuestState.Can_Start);
            }
        }
    }

    public Quest QuestStateCheck(int id)
    {
        return NewquestMap[id];
    }


    // todo : 
    private void StartQuest(int id)
    {
        Quest quest = GetQuestByID(id);
        Debug.Log("퀘스트스타트" + quest.info.displayName);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.In_Progress);
    }

    public void AdvanceQuest(int id)
    {
        Debug.Log(id);
        Quest quest = GetQuestByID(id);
        ChangeQuestState(quest.info.id, QuestState.Can_Finish);



        //현재 NextStep 구조 변경 진행중
        /*
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
        */

    }
    private void FinishQuest(int id)
    {
        Quest quest = GetQuestByID(id);

        //todo : 보상 지급
        //RewardManager.instance.Rewading(quest.info.EquipRewardID, quest.info.ConsumeRewardID, quest.info.GoldReward, quest.info.ExpReward);

        Debug.Log(quest + "퀘스트를 클리어했습니다");
        ChangeQuestState(quest.info.id, QuestState.Finished);
    }




    /*
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
    */
    

    public QuestData questData;
    public AllData datas;
    public QuestSlot questSlotPrefeb;
    public Transform tr;
    private Dictionary<int, Quest> CreatQuestMaps()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Quests/QuestData");
        datas = JsonUtility.FromJson<AllData>(jsonFile.text);
        Dictionary<int, Quest> idToQuestMap = new Dictionary<int, Quest>();
        foreach (QuestData questData in datas.quest)
        {
            idToQuestMap.Add(questData.id, new Quest(questData));
            print(questData.displayName + "를 추가했습니다.");
        }
        return idToQuestMap;
    }





    //직접 액세스하지 않고 이 메서드를 사용
    public Quest GetQuestByID(int id)
    {
        Quest quest = NewquestMap[id];
        if (quest == null) 
        {
            Debug.Log("questMap을 찾지 못함 " + id);
        }
        return quest;
    }

    //todo : 요구사항 충족 판별 메서드(현재 only 레벨)
    private bool CheckRequirements(Quest quest)
    {
        if (PlayerManager.Instance.playerLevel < quest.info.needLevel)
            return false;

        //todo : 전제조건 확인
        //QuestInfo.questPrerequisites

        return true;
    }



    //todo : ChangeQuestState() : 상태 변경 메서드
    private void ChangeQuestState(int id, QuestState state)
    {
        Quest quest = GetQuestByID(id);
        quest.state = state;
        NewquestMap[id].state = state;
        GameEventManager.instance.questEvent.QuestStateChange(quest);
    }
}
