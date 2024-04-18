using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;
using System.IO;

/*
 * QuestManager�� ��� ����Ʈ�� ���¸� ��ü������ ������.
 * �̺�Ʈ �߽�
 * Quest�� ID�� �߽����� ����, ����Ʈ �̵�(���� ����), Ŭ������ ���� ���� �̺�Ʈ
 * �÷��̾� ���� �� ���� ���Ź��� ����
 * QuestStateChange : ����Ʈ ���� �̺�Ʈ
 */
public class QuestManager : MonoBehaviour
{
    private static Dictionary<string, Quest> questMap;

    public static Dictionary<int, Quest> NewquestMap;

    //���డ�� �䱸���� ���� ������ ����

    public static QuestManager instance;

    public bool test;

    AudioManager audioManager;

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
        foreach (Quest quest in NewquestMap.Values)
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
        return NewquestMap[id];
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

        if (quest.info.repeatable == "�ݺ�")
        {
            ChangeQuestState(quest.info.id, QuestState.Can_Start);
            quest.info.questCurrentValue = 0;
            if (quest.info.questType == "CollectConsumeItem")
            {
                print(ItemManager.Instance.GetConsumeItem(quest.info.questValueID).data.consumeName);//ġ�� ����
                print(quest.info.questClearValue);
                ItemManager.Instance.ReduceConsumeItem(ItemManager.Instance.GetConsumeItem(quest.info.questValueID), quest.info.questClearValue);
            }
        }
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
                Debug.Log("ID : " + questInfo.id + " �� Error");
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));

            Debug.Log(questInfo.id + "����Ʈ �߰�");
        }
        return idToQuestMap;
    }
    */
    

    public QuestData questData;
    public AllData datas;
    public QuestSlot questSlotPrefeb;
    public int questCount;
    private Dictionary<int, Quest> CreatQuestMaps()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("QuestData");
        datas = JsonUtility.FromJson<AllData>(jsonFile.text);
        Dictionary<int, Quest> idToQuestMap = new Dictionary<int, Quest>();
        foreach (QuestData questData in datas.quest)
        {
            if (idToQuestMap.ContainsKey(questData.id))
            {
                Debug.Log($"{questData.id} �� Ű ���� ã�� �� ����.");
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



    //���� �׼������� �ʰ� �� �޼��带 ���
    public Quest GetQuestByID(int id)
    {
        Quest quest = NewquestMap[id];
        if (quest == null) 
        {
            Debug.Log("questMap�� ã�� ���� " + id);
        }
        return quest;
    }

    //todo : �䱸���� ���� �Ǻ� �޼���(���� only ����)
    private bool CheckRequirements(Quest quest)
    {
        if (PlayerManager.Instance.playerLevel >= quest.info.minLevel && PlayerManager.Instance.playerLevel <= quest.info.maxLevel)
            return true;

        return false;
    }

    private void OnApplicationQuit()
    {
        foreach (Quest quest in NewquestMap.Values)
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

    [SerializeField] private AllQuestSaveData Datas;
    // QuestSaveData questSaveData;
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

    
    //public Quest LoadQuest(QuestData questData)
    //{
    //    string FromJsonData = File.ReadAllText("Assets\\Resources\\Quests\\QuestSaveData.json");
    //    QuestSaveData questSaveData = JsonUtility.FromJson<QuestSaveData>(FromJsonData);
    //    return;
        //Quest quest = new Quest(QuestData questdata);
        //return quest;
        /*
        Quest quest = null;

        string serializedData = "asdf";

        QuestData info = JsonUtility.FromJson<QuestData>(serializedData);
        //quest = new Quest(info, ,);
        TextAsset jsonFile = Resources.Load<TextAsset>("Quests/QuestSaveData");
        if (jsonFile != null)
        {
            string json = jsonFile.text;
            tmp = JsonUtility.FromJson<QuestData>(json);
        }
        return quest;
        */

    //}

    private void ChangeQuestState(int id, QuestState state)
    {
        Quest quest = GetQuestByID(id);
        quest.state = state;
        NewquestMap[id].state = state;
        GameEventManager.instance.questEvent.QuestStateChange(quest);
    }


    //test�� ���� cheat button
    public void QuestClear()
    {
        foreach (Quest quest in NewquestMap.Values)
        {
            quest.info.questCurrentValue = 0;
            GameEventManager.instance.questEvent.QuestStateChange(quest);
            ChangeQuestState(quest.info.id, QuestState.Requirments_Not);
        }
    }
}
