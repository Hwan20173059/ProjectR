using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestContents : MonoBehaviour
{
    public List<Quest> questList = new List<Quest>();

    public GameObject questSlotPrefeb;
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            questList.Add(QuestManager.instance.GetQuestByID(i));
        }
        QuestListing();
    }



    private void QuestListing()
    {
        for (int i = 0; i < questList.Count; i++)
        {
            QuestSlot questslot = Instantiate<GameObject>(questSlotPrefeb, this.transform).GetComponent<QuestSlot>();
            questslot.questId = i;
        }
    }
}
