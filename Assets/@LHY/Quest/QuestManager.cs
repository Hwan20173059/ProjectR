using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;
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
    private int currentPlayerLevel = 1;//�����ʿ�

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
        Debug.Log("����Ʈ��ŸƮ" + quest.info.displayName);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.In_Progress);
    }

    public void AdvanceQuest(int id)
    {
        Debug.Log(id);
        Quest quest = GetQuestByID(id);
        ChangeQuestState(quest.info.id, QuestState.Can_Finish);



        //���� NextStep ���� ���� ������
        /*
        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            Debug.Log("���� ����");
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            Debug.Log("�Ϸᰡ��");
            ChangeQuestState(quest.info.id, QuestState.Can_Finish);
        }
        */

    }
    private void FinishQuest(int id)
    {
        Quest quest = GetQuestByID(id);

        //todo : ���� ����
        //RewardManager.instance.Rewading(quest.info.EquipRewardID, quest.info.ConsumeRewardID, quest.info.GoldReward, quest.info.ExpReward);

        Debug.Log(quest + "����Ʈ�� Ŭ�����߽��ϴ�");
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
    public Transform tr;
    private Dictionary<int, Quest> CreatQuestMaps()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Quests/QuestData");
        datas = JsonUtility.FromJson<AllData>(jsonFile.text);
        Dictionary<int, Quest> idToQuestMap = new Dictionary<int, Quest>();
        foreach (QuestData questData in datas.quest)
        {
            idToQuestMap.Add(questData.id, new Quest(questData));
            print(questData.displayName + "�� �߰��߽��ϴ�.");
        }
        return idToQuestMap;
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
        if (PlayerManager.Instance.playerLevel < quest.info.needLevel)
            return false;

        //todo : �������� Ȯ��
        //QuestInfo.questPrerequisites

        return true;
    }



    //todo : ChangeQuestState() : ���� ���� �޼���
    private void ChangeQuestState(int id, QuestState state)
    {
        Quest quest = GetQuestByID(id);
        quest.state = state;
        NewquestMap[id].state = state;
        GameEventManager.instance.questEvent.QuestStateChange(quest);
    }
}
