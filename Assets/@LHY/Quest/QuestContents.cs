using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestContents : MonoBehaviour
{
    public List<Quest> questList = new List<Quest>();

    public GameObject questSlotPrefeb;
    public int count;
    public GameObject canStartButton;
    public GameObject InProgressButton;
    public GameObject FinishedButton;

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
        ButtonColorChange(canStartButton);
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
        ButtonColorChange(InProgressButton);
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
        ButtonColorChange(FinishedButton);
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

    public void ButtonColorChange(GameObject gameObject)
    {
        canStartButton.GetComponent<Image>().color = Color.white;
        InProgressButton.GetComponent<Image>().color = Color.white;
        FinishedButton.GetComponent<Image>().color = Color.white;

        gameObject.GetComponent<Image>().color = Color.gray;
    }
}
