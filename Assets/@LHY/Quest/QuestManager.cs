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


    //���డ�� �䱸���� ���� ������ ����
    private int currentPlayerLevel = 1;//�����ʿ�

    public static QuestManager instance;


    public QuestStep[] questSteps;//todo : selectQuestSlots

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
            if ((quest.state == QuestState.Requirments_Not || quest.state == QuestState.Can_Start) && CheckRequirements(quest))
            {
                //Debug.Log(quest.state);
                GameEventManager.instance.questEvent.QuestStateChange(quest);
                ChangeQuestState(quest.info.id, QuestState.Can_Start);
            }
        }
    }

    public void QuestStateCheck(Quest quest)
    {
        quest.state = QuestState.Can_Start;
    }

    // todo : 
    private void StartQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        Debug.Log("����Ʈ��ŸƮ" + id);
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
            Debug.Log("���� ����");
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            Debug.Log("�Ϸᰡ��");
            ChangeQuestState(quest.info.id, QuestState.Can_Finish);
        }
    }
    private void FinishQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        //todo : ���� ����
        Debug.Log(quest + "����Ʈ�� Ŭ�����߽��ϴ�");
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
                Debug.Log("ID : " + questInfo.id + " �� Error");
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));

            Debug.Log(questInfo.id + "����Ʈ �߰�");
        }
        return idToQuestMap;
    }

    //���� �׼������� �ʰ� �� �޼��带 ���
    private Quest GetQuestByID(string id)
    {
        Quest quest = questMap[id];
        if (quest == null) 
        {
            Debug.Log("questMap�� ã�� ���� " + id);
        }
        return quest;
    }

    //todo : �䱸���� ���� �Ǻ� �޼���(���� only ����)
    private bool CheckRequirements(Quest quest)
    {
        //������ Ȯ���Ѵ�.
        if (currentPlayerLevel < quest.info.levelRequirement)
            return false;

        //todo : �������� Ȯ��
        //QuestInfo.questPrerequisites

        return true;
    }



    //todo : ChangeQuestState() : ���� ���� �޼���
    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestByID(id);
        quest.state = state;
        GameEventManager.instance.questEvent.QuestStateChange(quest);
    }
}
