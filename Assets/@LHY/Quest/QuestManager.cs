using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;
using System.IO;

public class QuestManager : MonoBehaviour
{
    public static Dictionary<int, Quest> questMap;

    //진행가능 요구사항 현재 레벨만 구현

    public static QuestManager instance;

    public bool test;
    [SerializeField] private AllQuestSaveData Datas;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        questMap = CreatQuestMap();
        if (File.Exists(Application.persistentDataPath + "/QuestSaveData.json"))
            LoadQuest();
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
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.Requirments_Not && CheckRequirements(quest))
            {
                GameEventManager.instance.questEvent.QuestStateChange(quest);
                GameEventManager.instance.questEvent.QuestNofication();
                ChangeQuestState(quest.info.id, QuestState.Can_Start);
            }
        }
    }

    public Quest QuestStateCheck(int id)
    {
        return questMap[id];
    }


    // todo : 
    private void StartQuest(int id)
    {
        Quest quest = GetQuestByID(id);
        quest.InstantiateCurrentQuestStep(this.transform, id);
        ChangeQuestState(quest.info.id, QuestState.In_Progress);
    }

    public void AdvanceQuest(int id)
    {
        Quest quest = GetQuestByID(id);
        ChangeQuestState(quest.info.id, QuestState.Can_Finish);
    }

    private void FinishQuest(int id)
    {
        Quest quest = GetQuestByID(id);

        RewardManager.instance.QuestRewardPopup(quest.info.goldReward, quest.info.expReward, -1);

        ChangeQuestState(quest.info.id, QuestState.Finished);
        GameEventManager.instance.questEvent.QuestNofication();

        if (quest.info.repeatable == "반복")
        {
            ChangeQuestState(quest.info.id, QuestState.Can_Start);
            quest.info.questCurrentValue = 0;
        }
        if (quest.info.questType == "CollectConsumeItem")
        {
            ConsumeItem c = ItemManager.Instance.cInventory.Find(i => i.data.id == quest.info.questValueID);
            ItemManager.Instance.ReduceConsumeItem(c, quest.info.questClearValue);
        }
    }

    public QuestData questData;
    public AllData datas;
    public int questCount;
    private Dictionary<int, Quest> CreatQuestMap()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("QuestData");
        datas = JsonUtility.FromJson<AllData>(jsonFile.text);
        Dictionary<int, Quest> idToQuestMap = new Dictionary<int, Quest>();
        foreach (QuestData questData in datas.quest)
        {
            if (idToQuestMap.ContainsKey(questData.id))
            {
                //Debug.Log($"{questData.id} 의 키 값을 찾을 수 없음.");
            }
            idToQuestMap.Add(questData.id, new Quest(questData));
            questCount++;
        }
        return idToQuestMap;
    }

    private void LoadQuest()
    {
        string FromJsonData = File.ReadAllText(Application.persistentDataPath + "/QuestSaveData.json");
        AllQuestSaveData allQuestSaveData = JsonUtility.FromJson<AllQuestSaveData>(FromJsonData);

        foreach (SaveQuestData saveQuestData in allQuestSaveData.questSaveData)
        {
            if (saveQuestData.questState == QuestState.In_Progress)
            {
                StartQuest(saveQuestData.questID);
            }
            if (saveQuestData.questState == QuestState.Can_Finish)
            {
                AdvanceQuest(saveQuestData.questID);
            }
            if (saveQuestData.questState == QuestState.Finished)
            {
                ChangeQuestState(saveQuestData.questID, saveQuestData.questState);
            }
            //ChangeQuestState(saveQuestData.questID, saveQuestData.questState);
            GetQuestByID(saveQuestData.questID).info.questCurrentValue = saveQuestData.questCurrentValue;
        }
    }



    //직접 액세스하지 않고 이 메서드를 사용
    public Quest GetQuestByID(int id)
    {
        Quest quest = questMap[id];
        if (quest == null) 
        {
            //Debug.Log("questMap을 찾지 못함 " + id);
        }
        return quest;
    }

    //todo : 요구사항 충족 판별 메서드(현재 only 레벨)
    private bool CheckRequirements(Quest quest)
    {
        if (PlayerManager.Instance.playerLevel >= quest.info.minLevel && PlayerManager.Instance.playerLevel <= quest.info.maxLevel)
            return true;

        return false;
    }

    private void OnApplicationQuit()
    {
        if (PlayerManager.Instance.isReset == false)
        {
            foreach (Quest quest in questMap.Values)
            {
                /*
                if (quest.info.questState == QuestState.Requirments_Not || 
                    quest.info.questState == QuestState.Can_Start)
                {
                    Debug.Log("yet start");
                }
                */
                SaveQuest(quest);
            }
            File.WriteAllText(Application.persistentDataPath + "/QuestSaveData.json", JsonUtility.ToJson(Datas));
        }
    }

    public void SaveQuest(Quest quest)
    {
        SaveQuestData saveQuestData = new SaveQuestData
        {
            questState = quest.state,
            questID = quest.info.id,
            questCurrentValue = quest.info.questCurrentValue
        };
        Datas.questSaveData.Add(saveQuestData);
    }

    private void ChangeQuestState(int id, QuestState state)
    {
        Quest quest = GetQuestByID(id);
        quest.state = state;
        questMap[id].state = state;
        GameEventManager.instance.questEvent.QuestStateChange(quest);
    }


    //test를 위한 cheat button
    public void QuestClear()
    {
        foreach (Quest quest in questMap.Values)
        {
            quest.info.questCurrentValue = 0;
            GameEventManager.instance.questEvent.QuestStateChange(quest);
            ChangeQuestState(quest.info.id, QuestState.Requirments_Not);
        }
    }
}
