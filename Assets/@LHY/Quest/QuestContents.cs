using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestContents : MonoBehaviour
{
    public List<Quest> questList = new List<Quest>();

    public GameObject questSlotPrefeb;
    public int count;
    private void Start()
    {
        for (int i = 0; i < 27; i++)
        {
            questList.Add(QuestManager.instance.GetQuestByID(i));
        }
        QuestListing();
        count = gameObject.transform.childCount;
        CanStartStateSelect();
    }


    private void QuestListing()
    {
        for (int i = 0; i < questList.Count; i++)
        {
            QuestSlot questslot = Instantiate<GameObject>(questSlotPrefeb, this.transform).GetComponent<QuestSlot>();
            questslot.questId = i;
        }
    }

    public void CanStartStateSelect()
    {
        QuestSelectClear();
        QuestSlot[] allChildren = GetComponentsInChildren<QuestSlot>();
        foreach (QuestSlot child in allChildren)
        {
            QuestState state = QuestManager.instance.GetQuestByID(child.questId).state;
            child.gameObject.SetActive(true);
            if (state != QuestState.Can_Start)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void ProgressStateSelect()
    {
        QuestSelectClear();
        QuestSlot[] allChildren = GetComponentsInChildren<QuestSlot>();
        foreach (QuestSlot child in allChildren)
        {
            QuestState state = QuestManager.instance.GetQuestByID(child.questId).state;
            child.gameObject.SetActive(true);
            if (state != QuestState.In_Progress && state != QuestState.Can_Finish)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void FinishedStateSelect()
    {
        QuestSelectClear();
        QuestSlot[] allChildren = GetComponentsInChildren<QuestSlot>();
        foreach (QuestSlot child in allChildren)
        {
            QuestState state = QuestManager.instance.GetQuestByID(child.questId).state;
            child.gameObject.SetActive(true);
            if (state != QuestState.Finished)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void QuestSelectClear()
    {
        int count = gameObject.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
